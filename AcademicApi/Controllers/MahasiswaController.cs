using AcademicApi.Data;
using AcademicApi.DTOs;
using AcademicApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcademicApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MahasiswaController : ControllerBase
{
    private readonly AcademicDbContext _context;

    public MahasiswaController(AcademicDbContext context)
    {
        _context = context;
    }

    // Query dasar yang dipakai ulang: mengubah Mahasiswa menjadi DTO
    // beserta nama dosen wali (tanpa perulangan objek JSON).
    private IQueryable<MahasiswaResponseDto> QueryResponse()
    {
        return _context.Mahasiswa
            .OrderBy(m => m.MahasiswaId)
            .Select(m => new MahasiswaResponseDto
            {
                MahasiswaId = m.MahasiswaId,
                Nim = m.Nim,
                NamaMahasiswa = m.NamaMahasiswa,
                Email = m.Email,
                Angkatan = m.Angkatan,
                DosenId = m.DosenId,
                NamaDosenWali = m.Dosen!.NamaDosen
            });
    }

    // GET: api/mahasiswa
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MahasiswaResponseDto>>> GetMahasiswa()
    {
        return Ok(await QueryResponse().ToListAsync());
    }

    // GET: api/mahasiswa/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MahasiswaResponseDto>> GetMahasiswaById(int id)
    {
        var response = await QueryResponse().FirstOrDefaultAsync(r => r.MahasiswaId == id);

        if (response == null)
        {
            return NotFound(new { message = "Data mahasiswa tidak ditemukan." });
        }

        return Ok(response);
    }

    // POST: api/mahasiswa
    [HttpPost]
    public async Task<ActionResult<MahasiswaResponseDto>> PostMahasiswa(MahasiswaRequestDto request)
    {
        // DosenId = dosen yang ada
        if (!await _context.Dosen.AnyAsync(d => d.DosenId == request.DosenId))
        {
            return BadRequest(new { message = "Dosen wali tidak ditemukan. Pilih dosen wali yang tersedia." });
        }

        // NIM unik
        if (await _context.Mahasiswa.AnyAsync(m => m.Nim == request.Nim))
        {
            return Conflict(new { message = "NIM sudah terdaftar." });
        }

        var mahasiswa = new Mahasiswa
        {
            Nim = request.Nim,
            NamaMahasiswa = request.NamaMahasiswa,
            Email = request.Email,
            Angkatan = request.Angkatan,
            DosenId = request.DosenId
        };

        _context.Mahasiswa.Add(mahasiswa);
        await _context.SaveChangesAsync();

        var response = await QueryResponse().FirstAsync(r => r.MahasiswaId == mahasiswa.MahasiswaId);

        return CreatedAtAction(nameof(GetMahasiswaById), new { id = mahasiswa.MahasiswaId }, response);
    }

    // PUT: api/mahasiswa/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMahasiswa(int id, MahasiswaRequestDto request)
    {
        var mahasiswa = await _context.Mahasiswa.FindAsync(id);
        if (mahasiswa == null)
        {
            return NotFound(new { message = "Data mahasiswa tidak ditemukan." });
        }

        if (!await _context.Dosen.AnyAsync(d => d.DosenId == request.DosenId))
        {
            return BadRequest(new { message = "Dosen wali tidak ditemukan. Pilih dosen wali yang tersedia." });
        }

        // NIM unik
        if (await _context.Mahasiswa.AnyAsync(m => m.Nim == request.Nim && m.MahasiswaId != id))
        {
            return Conflict(new { message = "NIM sudah terdaftar pada mahasiswa lain." });
        }

        mahasiswa.Nim = request.Nim;
        mahasiswa.NamaMahasiswa = request.NamaMahasiswa;
        mahasiswa.Email = request.Email;
        mahasiswa.Angkatan = request.Angkatan;
        mahasiswa.DosenId = request.DosenId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/mahasiswa/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMahasiswa(int id)
    {
        var mahasiswa = await _context.Mahasiswa.FindAsync(id);
        if (mahasiswa == null)
        {
            return NotFound(new { message = "Data mahasiswa tidak ditemukan." });
        }

        _context.Mahasiswa.Remove(mahasiswa);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}