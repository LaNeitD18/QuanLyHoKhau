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
    
    public partial class GIAYTAMTRU
    {
        public string MaGiayTamTru { get; set; }
        public string MaSoLuuTamTru { get; set; }
        public string MaCongAn { get; set; }
        public string CMND { get; set; }
        public Nullable<System.DateTime> NgayKhaiBao { get; set; }
        public Nullable<System.DateTime> NgayBatDau { get; set; }
        public Nullable<System.DateTime> NgayKetThuc { get; set; }
        public string LyDo { get; set; }
        public string NoiDangKyTamTru { get; set; }
        public string MaGiayTamVang { get; set; }
    
        public virtual CONGAN CONGAN { get; set; }
        public virtual PHIEUKHAIBAOTAMVANG PHIEUKHAIBAOTAMVANG { get; set; }
        public virtual SOLUUTAMTRU SOLUUTAMTRU { get; set; }
        public virtual NHANKHAU NHANKHAU { get; set; }
    }
}
