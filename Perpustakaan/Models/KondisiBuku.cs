using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Models
{
    public partial class KondisiBuku
    {
        public KondisiBuku()
        {
            Pengembalian = new HashSet<Pengembalian>();
        }

        public int NoKondisi { get; set; }
        [Required(ErrorMessage = "Nama Kondisi tidak boleh kosong!!")]
        public string NamaKondisi { get; set; }

        public ICollection<Pengembalian> Pengembalian { get; set; }
    }
}
