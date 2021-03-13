using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Models
{ 
    /// <summary>
    /// Main Class
    /// </summary>
    /// <remarks>
    /// Class ini digunakan untuk verifikasi data yang akan dimasukan
    /// </remarks>
    public partial class Buku
    {
        /// <summary>
        /// Class Buku
        /// </summary>
        public Buku()
        {
            Peminjaman = new HashSet<Peminjaman>();
        }
        /// <summary>
        /// Untuk verifikasi data nomor buku
        /// </summary>
        [Required(ErrorMessage = "No Buku tidak boleh kosong!!")]
        public int NoBuku { get; set; }

        /// <summary>
        /// Untuk verifikasi data judul buku
        /// </summary>
        [Required(ErrorMessage = "Judul Buku tidak boleh kosong!!")]
        public string JudulBuku { get; set; }

        /// <summary>
        /// Untuk verifikasi data nomor katalog
        /// </summary>
        [Required(ErrorMessage = "No Katalog tidak boleh kosong!!")]
        public int? NoKatalog { get; set; }

        /// <summary>
        /// Untuk verifikasi data nomor rak
        /// </summary>
        [Required(ErrorMessage = "No Rak tidak boleh kosong!!")]
        public int? NoRak { get; set; }

        /// <summary>
        /// Untuk verifikasi data pengarang
        /// </summary>
        [Required(ErrorMessage = "Pengarang tidak boleh kosong!!")]
        public string Pengarang { get; set; }

        /// <summary>
        /// Untuk verifikasi data penerbit
        /// </summary>
        [Required(ErrorMessage = "Penerbit tidak boleh kosong!!")]
        public string Penerbit { get; set; }


        public KatalogBuku NoKatalogNavigation { get; set; }
        public Rak NoRakNavigation { get; set; }
        public ICollection<Peminjaman> Peminjaman { get; set; }
    }
}
