using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Models
{
    public partial class Peminjaman
    {
        public Peminjaman()
        {
            Pengembalian = new HashSet<Pengembalian>();
        }

        public int NoPeminjaman { get; set; }
        [Required(ErrorMessage = "Tanggal Peminjaman tidak boleh kosong!!")]
        public DateTime? TglPeminjaman { get; set; }
        [Required(ErrorMessage = "No Buku tidak boleh kosong!!")]
        public int? NoBuku { get; set; }
        [Required(ErrorMessage = "No Anggota tidak boleh kosong!!")]
        public int? NoAnggota { get; set; }

        public Mahasiswa NoAnggotaNavigation { get; set; }
        public Buku NoBukuNavigation { get; set; }
        public ICollection<Pengembalian> Pengembalian { get; set; }
    }
}
