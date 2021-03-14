using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Models
{
    /// <summary>
    /// Main Class
    /// </summary>
    /// <remarks>
    /// Class Pengembalian digunakan untuk verifikasi data pengembalian yang akan dimasukan
    /// </remarks>
    public partial class Pengembalian
    {
        /// <summary>
        /// Untuk verifikasi Nomor Pengembalian
        /// </summary>
        public int NoPengembalian { get; set; }
        /// <summary>
        /// Untuk verifikasi Tanggal pengembalian
        /// </summary>
        [Required(ErrorMessage = "Tanggal Pengembalian tidak boleh kosong!!")]
        public DateTime? TglPengembalian { get; set; }
        /// <summary>
        /// Untuk verifikasi Nomor kondisi
        /// </summary>
        [Required(ErrorMessage = "No Kondisi tidak boleh kosong!!")]
        public int? NoKondisi { get; set; }
        /// <summary>
        /// Untuk verifikasi Denda
        /// </summary>
        [Required(ErrorMessage = "Denda tidak boleh kosong!!")]
        public int? Denda { get; set; }
        /// <summary>
        /// Untuk verifikasi Nomor peminjaman
        /// </summary>
        [Required(ErrorMessage = "No Peminjaman tidak boleh kosong!!")]
        public int? NoPeminjaman { get; set; }

        public KondisiBuku NoKondisiNavigation { get; set; }
        public Peminjaman NoPeminjamanNavigation { get; set; }
    }
}
