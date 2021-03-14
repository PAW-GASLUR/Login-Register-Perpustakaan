using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Models
{
    /// <summary>
    /// Main Class
    /// </summary>
    /// <remarks>
    /// Class KondisiBuku digunakan untuk verifikasi data kondisi buku yang akan dimasukan
    /// </remarks>
    public partial class KondisiBuku
    {
        /// <summary>
        /// Class Kondisi Buku
        /// </summary>
        public KondisiBuku()
        {
            Pengembalian = new HashSet<Pengembalian>();
        }

        /// <summary>
        /// Untuk verifikasi nomor kondisi
        /// </summary>
        public int NoKondisi { get; set; }
        /// <summary>
        /// untuk verifikasi data Kondisi buku
        /// </summary>
        [Required(ErrorMessage = "Nama Kondisi tidak boleh kosong!!")]
        public string NamaKondisi { get; set; }

        public ICollection<Pengembalian> Pengembalian { get; set; }
    }
}
