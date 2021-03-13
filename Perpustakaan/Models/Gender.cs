using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Models
{
    /// <summary>
    /// Main Class
    /// </summary>
    /// <remarks>
    /// Class Gender dapat memverifikasi data gender yang dimasukkan
    /// </remarks>
    public partial class Gender
    {
        /// <summary>
        /// Class gender
        /// </summary>
        public Gender()
        {
            Mahasiswa = new HashSet<Mahasiswa>();
        }

        /// <summary>
        /// Untuk verifikasi data nomor gender
        /// </summary>
        public int NoGender { get; set; }

        /// <summary>
        /// Untuk verifikasi data gender atau jenis kelamin
        /// </summary>
        [Required(ErrorMessage = "Nama Gender tidak boleh kosong!!")]
        public string NamaGender { get; set; }

        public ICollection<Mahasiswa> Mahasiswa { get; set; }
    }
}
