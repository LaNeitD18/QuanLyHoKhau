//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyHoKhau.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class NHANKHAUCHODUYET
    {
        public string MaNhanKhau { get; set; }
        public string MASHK { get; set; }
        public string MaNguoi { get; set; }
        public string QuanHeVoiChuHo { get; set; }
        public string ChoOHienNay { get; set; }
        public string ActionType { get; set; }
    
        public virtual NGUOI NGUOI { get; set; }
        public virtual SOHOKHAU SOHOKHAU { get; set; }
    }
}
