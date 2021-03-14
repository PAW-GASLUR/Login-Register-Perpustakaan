using System;

namespace Perpustakaan.Models
{
    /// <summary>
    /// Main Class
    /// </summary>
    /// <remarks>
    /// Class error view digunakan untuk menampilkan error
    /// </remarks>
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
    ///<summary>
    ///Function untuk GET data error
    /// </summary>
}