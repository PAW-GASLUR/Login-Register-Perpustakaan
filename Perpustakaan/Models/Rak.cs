using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Models
{
    public partial class Rak
    {
        public Rak()
        {
            Buku = new HashSet<Buku>();
        }

        public int NoRak { get; set; }
        [Required(ErrorMessage = "Nama Rak tidak boleh kosong!!")]
        public string NamaRak { get; set; }

        public ICollection<Buku> Buku { get; set; }
    }
}
