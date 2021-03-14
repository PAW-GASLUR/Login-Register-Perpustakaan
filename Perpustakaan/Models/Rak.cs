using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Models
{
    /// <summary>
    /// Main Class
    /// </summary>
    /// <remarks>
    /// Class Rak digunakan untuk verifikasi data Rak yang akan dimasukan
    /// </remarks>
    public partial class Rak
    {
        /// <summary>
        /// Class Rak
        /// </summary>
        public Rak()
        {
            Buku = new HashSet<Buku>();
        }

        /// <summary>
        /// Untuk verikasi Nomor Rak
        /// </summary>
        public int NoRak { get; set; }
        /// <summary>
        /// Untuk verifikasi Nama Rak
        /// </summary>
        [Required(ErrorMessage = "Nama Rak tidak boleh kosong!!")]
        public string NamaRak { get; set; }

        public ICollection<Buku> Buku { get; set; }
    }
}
