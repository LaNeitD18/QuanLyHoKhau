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
    
    public partial class CONGAN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CONGAN()
        {
            this.GIAYTAMTRUs = new HashSet<GIAYTAMTRU>();
            this.PHIEUKHAIBAOTAMVANGs = new HashSet<PHIEUKHAIBAOTAMVANG>();
            this.PHIEUTHAYDOI_HK_NK = new HashSet<PHIEUTHAYDOI_HK_NK>();
            this.SOHOKHAUs = new HashSet<SOHOKHAU>();
            this.SOHOKHAUCHODUYETs = new HashSet<SOHOKHAUCHODUYET>();
        }
    
        public string MaCongAn { get; set; }
        public string MaNguoi { get; set; }
        public string MaLoaiCongAn { get; set; }
    
        public virtual LOAICONGAN LOAICONGAN { get; set; }
        public virtual NGUOI NGUOI { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GIAYTAMTRU> GIAYTAMTRUs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUKHAIBAOTAMVANG> PHIEUKHAIBAOTAMVANGs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUTHAYDOI_HK_NK> PHIEUTHAYDOI_HK_NK { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SOHOKHAU> SOHOKHAUs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SOHOKHAUCHODUYET> SOHOKHAUCHODUYETs { get; set; }
    }
}
