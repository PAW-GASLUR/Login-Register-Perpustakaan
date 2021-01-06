using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Models
{
    public partial class Pengembalian
    {
        public int NoPengembalian { get; set; }
        [Required(ErrorMessage = "Tanggal Pengembalian tidak boleh kosong!!")]
        public DateTime? TglPengembalian { get; set; }
        [Required(ErrorMessage = "No Kondisi tidak boleh kosong!!")]
        public int? NoKondisi { get; set; }
        [Required(ErrorMessage = "Denda tidak boleh kosong!!")]
        public int? Denda { get; set; }
        [Required(ErrorMessage = "No Peminjaman tidak boleh kosong!!")]
        public int? NoPeminjaman { get; set; }

        public KondisiBuku NoKondisiNavigation { get; set; }
        public Peminjaman NoPeminjamanNavigation { get; set; }
    }
}
