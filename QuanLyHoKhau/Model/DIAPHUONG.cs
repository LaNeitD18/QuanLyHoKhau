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
    
    public partial class DIAPHUONG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DIAPHUONG()
        {
            this.CONGANs = new HashSet<CONGAN>();
            this.SOLUUNHANKHAUs = new HashSet<SOLUUNHANKHAU>();
            this.SOLUUTAMTRUs = new HashSet<SOLUUTAMTRU>();
            this.SOLUUTAMVANGs = new HashSet<SOLUUTAMVANG>();
        }
    
        public string MaDiaPhuong { get; set; }
        public string TenDiaPhuong { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONGAN> CONGANs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SOLUUNHANKHAU> SOLUUNHANKHAUs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SOLUUTAMTRU> SOLUUTAMTRUs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SOLUUTAMVANG> SOLUUTAMVANGs { get; set; }
    }
}
