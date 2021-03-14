using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Models
{/// <summary>
/// Main Class
/// </summary>
/// <remarks>
/// Class Mahasiswa digunakan untuk verifikasi data mahasiswa yang akan dimasukan
/// </remarks>
    public partial class Mahasiswa
    {
        /// <summary>
        /// Class Mahasiswa
        /// </summary>
        public Mahasiswa()
        {
            Peminjaman = new HashSet<Peminjaman>();
        }


        /// <summary>
        /// untuk verifikasi data no anggota
        /// </summary>
        public int NoAnggota { get; set; }
        /// <summary>
        /// untuk verifikasi nomor induk mahasiswa
        /// </summary>
        [Required(ErrorMessage = "NIM tidak boleh kosong!!")]
        public string Nim { get; set; }
        /// <summary>
        /// Untuk verifikasi nama
        /// </summary>
        [Required(ErrorMessage = "Nama tidak boleh kosong!!")]
        public string Nama { get; set; }
        /// <summary>
        /// Untuk verifikasi no gender
        /// </summary>
        [Required(ErrorMessage = "No Gender tidak boleh kosong!!")]
        public int? NoGender { get; set; }
        /// <summary>
        /// Untuk verifikasi no HP
        /// </summary>
        [Required(ErrorMessage = "No Hp tidak boleh kosong!!")]
        public string NoHp { get; set; }
        /// <summary>
        /// Untuk verifikasi Alamat
        /// </summary>
        [Required(ErrorMessage = "Alamat tidak boleh kosong!!")]
        public string Alamat { get; set; }

        public Gender NoGenderNavigation { get; set; }
        public ICollection<Peminjaman> Peminjaman { get; set; }
    }
}
