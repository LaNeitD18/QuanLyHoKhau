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
    
    public partial class SOLUUCHUYENKHAU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SOLUUCHUYENKHAU()
        {
            this.PHIEUDUYETCHUYENKHAUs = new HashSet<PHIEUDUYETCHUYENKHAU>();
        }
    
        public string MaSoLuuChuyenKhau { get; set; }
        public string DiaPhuong { get; set; }
    
        public virtual DIAPHUONG DIAPHUONG1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUDUYETCHUYENKHAU> PHIEUDUYETCHUYENKHAUs { get; set; }
    }
}
