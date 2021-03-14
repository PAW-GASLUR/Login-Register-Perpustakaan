using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Models
{
    /// <summary>
    /// Main Class
    /// </summary>
    /// <remarks>
    /// Class Peminjaman digunakan untuk verifikasi data Peminjaman buku yang akan dimasukan
    /// </remarks>
    public partial class Peminjaman
    {
       

        /// <summary>
        /// Class Peminjaman 
        /// </summary>
        public Peminjaman()
        {
            Pengembalian = new HashSet<Pengembalian>();
        }

        /// <summary>
        /// Untuk verifiaksi Nomor peminjaman
        /// </summary>
        public int NoPeminjaman { get; set; }
        /// <summary>
        /// Untuk verifikasi Tanggal Peminjaman
        /// </summary>
        [Required(ErrorMessage = "Tanggal Peminjaman tidak boleh kosong!!")]
        public DateTime? TglPeminjaman { get; set; }
        /// <summary>
        /// Untuk verifikasi Nomor Buku
        /// </summary>
        [Required(ErrorMessage = "No Buku tidak boleh kosong!!")]
        public int? NoBuku { get; set; }
        /// <summary>
        /// Untuk verifikasi Nomor Anggota
        /// </summary>
        [Required(ErrorMessage = "No Anggota tidak boleh kosong!!")]
        public int? NoAnggota { get; set; }

        public Mahasiswa NoAnggotaNavigation { get; set; }
        public Buku NoBukuNavigation { get; set; }
        public ICollection<Pengembalian> Pengembalian { get; set; }
    }
}
