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
    
    public partial class SOHOKHAU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SOHOKHAU()
        {
            this.NHANKHAUs = new HashSet<NHANKHAU>();
            this.PHIEUDUYETSOHOKHAUs = new HashSet<PHIEUDUYETSOHOKHAU>();
            this.PHIEUDUYETSOHOKHAUs1 = new HashSet<PHIEUDUYETSOHOKHAU>();
        }
    
        public string MaSHK { get; set; }
        public string CMNDChuHo { get; set; }
        public string MaCongAn { get; set; }
        public string MaSoLuuNhanKhau { get; set; }
        public string DiaChi { get; set; }
        public System.DateTime NgayTaoSo { get; set; }
        public bool BanChinhThuc { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual CONGAN CONGAN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHANKHAU> NHANKHAUs { get; set; }
        public virtual NHANKHAU NHANKHAU { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUDUYETSOHOKHAU> PHIEUDUYETSOHOKHAUs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUDUYETSOHOKHAU> PHIEUDUYETSOHOKHAUs1 { get; set; }
        public virtual SOLUUNHANKHAU SOLUUNHANKHAU { get; set; }
    }
}
