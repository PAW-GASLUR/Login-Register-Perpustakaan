using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Models
{
    public partial class Mahasiswa
    {
        public Mahasiswa()
        {
            Peminjaman = new HashSet<Peminjaman>();
        }


        public int NoAnggota { get; set; }
        [Required(ErrorMessage = "NIM tidak boleh kosong!!")]
        public string Nim { get; set; }
        [Required(ErrorMessage = "Nama tidak boleh kosong!!")]
        public string Nama { get; set; }
        [Required(ErrorMessage = "No Gender tidak boleh kosong!!")]
        public int? NoGender { get; set; }
        [Required(ErrorMessage = "No Hp tidak boleh kosong!!")]
        public string NoHp { get; set; }
        [Required(ErrorMessage = "Alamat tidak boleh kosong!!")]
        public string Alamat { get; set; }

        public Gender NoGenderNavigation { get; set; }
        public ICollection<Peminjaman> Peminjaman { get; set; }
    }
}
