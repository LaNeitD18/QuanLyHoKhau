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
    
    public partial class NGUOI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NGUOI()
        {
            this.CONGANs = new HashSet<CONGAN>();
            this.SOHOKHAUCHODUYETs = new HashSet<SOHOKHAUCHODUYET>();
        }
    
        public string CMND { get; set; }
        public string Ten { get; set; }
        public System.DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string NgheNghiep { get; set; }
        public string NoiSinh { get; set; }
        public string QueQuan { get; set; }
        public string DanToc { get; set; }
        public string TonGiao { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONGAN> CONGANs { get; set; }
        public virtual NHANKHAU NHANKHAU { get; set; }
        public virtual NHANKHAUCHODUYET NHANKHAUCHODUYET { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SOHOKHAUCHODUYET> SOHOKHAUCHODUYETs { get; set; }
    }
}
