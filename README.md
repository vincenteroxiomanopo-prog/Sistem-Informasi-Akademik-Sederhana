# Sistem Informasi Akademik Sederhana

Tugas Pengganti UTS Pemrograman Mobile 2026 — CRUD data dosen dan mahasiswa (relasi dosen wali).
Penulis: Vincente Roxio Manopo (NIM 72240690)

Arsitektur: .NET MAUI Blazor Hybrid (`AcademicHybridApp`) → HTTP/JSON → ASP.NET Core Web API (`AcademicApi`) → EF Core → SQLite.

## Fitur

- **a.** CRUD Dosen dan Mahasiswa dengan validasi, konfirmasi hapus, notifikasi, dan indikator loading. Dosen yang masih menjadi dosen wali tidak dapat dihapus.
- **b.** Dropdown dosen wali pada form mahasiswa diisi dari API dosen.
- **c.** Bonus: pencarian mahasiswa berdasarkan NIM atau nama (`GET /api/mahasiswa?cari=...`).
- **d.** Bonus: pengurutan daftar mahasiswa berdasarkan NIM, nama, atau angkatan (`GET /api/mahasiswa?urut=nama&desc=true`), dengan mengklik judul kolom.
- **e.** Bonus: Beranda menampilkan jumlah dosen dan mahasiswa dari data API.

## Prasyarat

- **a.** Windows 10/11 dengan Developer Mode aktif (diperlukan untuk menjalankan aplikasi MAUI di Windows).
- **b.** Visual Studio 2026 dengan workload *ASP.NET and web development* dan *.NET Multi-platform App UI development*.
- **c.** .NET 10 SDK (ikut terpasang bersama Visual Studio 2026).
- **d.** dotnet-ef: `dotnet tool install --global dotnet-ef`
- **e.** Git.

## Konfigurasi

- **a. Connection string:** `AcademicApi/appsettings.json`, nilai `Data Source=academic.db`. Database SQLite berupa satu file `academic.db` yang terbentuk di folder `AcademicApi/`.
- **b. Port API:** `AcademicApi/Properties/launchSettings.json`, profil `http`, yaitu `http://localhost:5072`.
- **c. Alamat API untuk aplikasi:** `AcademicHybridApp/Services/ApiSettings.cs`. Windows memakai `http://localhost:5072/`. Jika port API diubah, ubah di `launchSettings.json` dan `ApiSettings.cs`.

## Langkah menjalankan

- **a. Clone.** Buka Visual Studio 2026, klik **Clone a repository**, isi `https://github.com/vincenteroxiomanopo-prog/Sistem-Informasi-Akademik-Sederhana.git` dan folder kosong, lalu klik **Clone**.
- **b. Buka solution.** Klik dua kali `AcademicSystem.slnx`. Tunggu restore paket NuGet selesai (indikator di pojok kiri bawah). Jika Visual Studio menawarkan pemasangan workload yang kurang, setujui.
- **c. Buat database.** Buka **View → Terminal**, lalu jalankan perintah berikut. Perintah ini menerapkan migration pada database kosong dan mengisi data awal:
  ```
  dotnet ef database update --project AcademicApi
  ```
- **d. Atur startup.** Klik kanan Solution → **Configure Startup Projects → Multiple startup projects**. Set `AcademicApi` (profil `http`) dan `AcademicHybridApp` (target **Windows Machine**) ke *Start*.
- **e. Jalankan.** Tekan F5. Swagger dapat dibuka di http://localhost:5072/swagger

Alternatif tanpa multiple startup: jalankan API dari terminal dengan `dotnet run --project AcademicApi --launch-profile http`, lalu jalankan `AcademicHybridApp` (Windows Machine) dengan Ctrl+F5.

## Data uji

Dibuat otomatis oleh migration. Tidak ada akun login.

Dosen:
- **a.** 0512345678 — Dr. Anita Wijaya (Sistem Informasi)
- **b.** 0523456789 — Budi Hartono, M.Kom. (Informatika)
- **c.** 0534567890 — Citra Lestari, M.T. (Sistem Informasi)

Mahasiswa (dosen wali):
- **a.** 72230001 — Budi Santoso, 2023 (Dr. Anita Wijaya)
- **b.** 72230002 — Sari Wulandari, 2023 (Dr. Anita Wijaya)
- **c.** 72240001 — Dimas Prasetyo, 2024 (Budi Hartono, M.Kom.)
- **d.** 72240002 — Putri Maharani, 2024 (Budi Hartono, M.Kom.)
- **e.** 72220001 — Rizky Ananda, 2022 (Budi Hartono, M.Kom.)

## Batasan

- **a.** Diuji di Windows.
- **b.** Android (emulator dan perangkat asli) belum diuji.

## Sumber & alat bantu

- **a. AI:** Claude (Anthropic) dipakai sebagai pendamping belajar: menjelaskan konsep, memandu langkah, memberi contoh kode, membantu menelusuri error, dan menyusun README ini.
- **b. Dikerjakan sendiri:** memasukkan dan menyesuaikan kode, menjalankan dan menguji (Swagger dan aplikasi), menulis pesan commit, dan mempelajari kode agar dapat dijelaskan saat demo. AI bukan contributor repository.
- **c. Alat lain:** Visual Studio 2026, .NET 10, EF Core 10 (SQLite), Swashbuckle (Swagger UI), Bootstrap, ekstensi SQLite Toolbox.
- **d. Acuan:** dokumen tugas "UTS Pemrograman Mobile 2026 – Tugas Pengganti UTS".