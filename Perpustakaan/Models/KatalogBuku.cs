using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Models
{
    /// <summary>
    /// Main Class
    /// </summary>
    /// <remarks>
    /// Class katalogbuku digunakan untuk verifikasi data katalog buku yang akan dimasukkan
    /// </remarks>
    public partial class KatalogBuku
    {
        /// <summary>
        /// Class katalog buku
        /// </summary>
        public KatalogBuku()
        {
            Buku = new HashSet<Buku>();
        }
        
        /// <summary>
        /// Untuk Verifikasi data nomor katalog
        /// </summary>
        public int NoKatalog { get; set; }

        /// <summary>
        /// Untuk verifikasi data katalog buku
        /// </summary>
        [Required(ErrorMessage = "Jenis Katalog tidak boleh kosong!!")]
        public string JenisKatalog { get; set; }

        public ICollection<Buku> Buku { get; set; }
    }
}
