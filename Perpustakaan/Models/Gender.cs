using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Models
{
    public partial class Gender
    {
        public Gender()
        {
            Mahasiswa = new HashSet<Mahasiswa>();
        }

        public int NoGender { get; set; }
        [Required(ErrorMessage = "Nama Gender tidak boleh kosong!!")]
        public string NamaGender { get; set; }

        public ICollection<Mahasiswa> Mahasiswa { get; set; }
    }
}
