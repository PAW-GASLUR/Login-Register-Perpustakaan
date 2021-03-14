using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Models
{
    /// <summary>
    /// Main class 
    /// </summary>
    /// <remarks>
    /// Class Mahasiswa digunakan untuk verifikasi data mahasiswa yang akan ditambahkan
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
        /// Untuk verifikasi nomor anggota
        /// </summary>
        public int NoAnggota { get; set; }
       
        /// <summary>
        /// Untuk verifikasi nomor induk mahasiswa
        /// </summary>
        [Required(ErrorMessage = "NIM tidak boleh kosong!!")]
        public string Nim { get; set; }
       
        /// <summary>
        /// Untuk verifikasi nama mahasiswa
        /// </summary>
        [Required(ErrorMessage = "Nama tidak boleh kosong!!")]
        public string Nama { get; set; }
      
        /// <summary>
        /// Untuk verifikasi nomor gender mahasiswa
        /// </summary>
        [Required(ErrorMessage = "No Gender tidak boleh kosong!!")]
        public int? NoGender { get; set; }
       
        /// <summary>
        /// Untuk verifikasi nomor hp mahasiswa
        /// </summary>
        [Required(ErrorMessage = "No Hp tidak boleh kosong!!")]
        public string NoHp { get; set; }
       
        /// <summary>
        /// Untuk verifikasi alamat mahasiswa
        /// </summary>
        [Required(ErrorMessage = "Alamat tidak boleh kosong!!")]
        public string Alamat { get; set; }

        public Gender NoGenderNavigation { get; set; }
        public ICollection<Peminjaman> Peminjaman { get; set; }
    }
}
