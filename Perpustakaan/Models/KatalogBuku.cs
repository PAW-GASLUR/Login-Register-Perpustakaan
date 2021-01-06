using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Models
{
    public partial class KatalogBuku
    {
        public KatalogBuku()
        {
            Buku = new HashSet<Buku>();
        }

        public int NoKatalog { get; set; }
        [Required(ErrorMessage = "Jenis Katalog tidak boleh kosong!!")]
        public string JenisKatalog { get; set; }

        public ICollection<Buku> Buku { get; set; }
    }
}
