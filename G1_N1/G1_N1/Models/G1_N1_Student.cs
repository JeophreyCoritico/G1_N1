//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace G1_N1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class G1_N1_Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public G1_N1_Student()
        {
            this.G1_N1_Attendance = new HashSet<G1_N1_Attendance>();
        }
    
        public int Barcode { get; set; }
        public int GroupNumber { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public int StudentID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<G1_N1_Attendance> G1_N1_Attendance { get; set; }
        public virtual G1_N1_Group G1_N1_Group { get; set; }
    }
}
