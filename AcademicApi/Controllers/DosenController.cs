using AcademicApi.Data;
using AcademicApi.DTOs;
using AcademicApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcademicApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DosenController : ControllerBase
{
    private readonly AcademicDbContext _context;

    public DosenController(AcademicDbContext context)
    {
        _context = context;
    }

    // GET: api/dosen
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DosenResponseDto>>> GetDosen()
    {
        var dosenList = await _context.Dosen
            .Select(d => new DosenResponseDto
            {
                DosenId = d.DosenId,
                Nidn = d.Nidn,
                NamaDosen = d.NamaDosen,
                Email = d.Email,
                Prodi = d.Prodi
            })
            .ToListAsync();

        return Ok(dosenList);
    }

    // GET: api/dosen/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DosenResponseDto>> GetDosen(int id)
    {
        var dosen = await _context.Dosen.FindAsync(id);

        if (dosen == null)
        {
            return NotFound(new { message = "Data dosen tidak ditemukan." });
        }

        var response = new DosenResponseDto
        {
            DosenId = dosen.DosenId,
            Nidn = dosen.Nidn,
            NamaDosen = dosen.NamaDosen,
            Email = dosen.Email,
            Prodi = dosen.Prodi
        };

        return Ok(response);
    }

    // POST: api/dosen
    [HttpPost]
    public async Task<ActionResult<DosenResponseDto>> PostDosen(DosenRequestDto request)
    {
        // Cek keunikan NIDN (menghasilkan 409 Conflict)
        if (await _context.Dosen.AnyAsync(d => d.Nidn == request.Nidn))
        {
            return Conflict(new { message = "NIDN sudah terdaftar." });
        }

        var dosen = new Dosen
        {
            Nidn = request.Nidn,
            NamaDosen = request.NamaDosen,
            Email = request.Email,
            Prodi = request.Prodi
        };

        _context.Dosen.Add(dosen);
        await _context.SaveChangesAsync();

        var response = new DosenResponseDto
        {
            DosenId = dosen.DosenId,
            Nidn = dosen.Nidn,
            NamaDosen = dosen.NamaDosen,
            Email = dosen.Email,
            Prodi = dosen.Prodi
        };

        return CreatedAtAction(nameof(GetDosen), new { id = dosen.DosenId }, response);
    }

    // PUT: api/dosen/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDosen(int id, DosenRequestDto request)
    {
        var dosen = await _context.Dosen.FindAsync(id);
        if (dosen == null)
        {
            return NotFound(new { message = "Data dosen tidak ditemukan." });
        }

        // Cek keunikan NIDN, pastikan bukan milik dosen lain
        if (await _context.Dosen.AnyAsync(d => d.Nidn == request.Nidn && d.DosenId != id))
        {
            return Conflict(new { message = "NIDN sudah terdaftar pada dosen lain." });
        }

        dosen.Nidn = request.Nidn;
        dosen.NamaDosen = request.NamaDosen;
        dosen.Email = request.Email;
        dosen.Prodi = request.Prodi;

        await _context.SaveChangesAsync();

        return NoContent(); // Status 204
    }

    // DELETE: api/dosen/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDosen(int id)
    {
        var dosen = await _context.Dosen.FindAsync(id);
        if (dosen == null)
        {
            return NotFound(new { message = "Data dosen tidak ditemukan." });
        }

        _context.Dosen.Remove(dosen);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}