using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Models
{ /// nama project

    public partial class Buku
    {
        /// main class
        public Buku()
        {
            Peminjaman = new HashSet<Peminjaman>();
        }

        [Required(ErrorMessage = "No Buku tidak boleh kosong!!")]
        public int NoBuku { get; set; }
        [Required(ErrorMessage = "Judul Buku tidak boleh kosong!!")]
        public string JudulBuku { get; set; }
        [Required(ErrorMessage = "No Katalog tidak boleh kosong!!")]
        public int? NoKatalog { get; set; }
        [Required(ErrorMessage = "No Rak tidak boleh kosong!!")]
        public int? NoRak { get; set; }
        [Required(ErrorMessage = "Pengarang tidak boleh kosong!!")]
        public string Pengarang { get; set; }
        [Required(ErrorMessage = "Penerbit tidak boleh kosong!!")]
        public string Penerbit { get; set; }


        public KatalogBuku NoKatalogNavigation { get; set; }
        public Rak NoRakNavigation { get; set; }
        public ICollection<Peminjaman> Peminjaman { get; set; }
    }
}
