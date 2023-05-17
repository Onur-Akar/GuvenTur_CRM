namespace GuvenTur_CRM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Members
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Members()
        {
            Member_Payments = new HashSet<Member_Payments>();
            Members_Pay_Info = new HashSet<Members_Pay_Info>();
        }

        public int Id { get; set; }

        public int Member_Group_Id { get; set; }

        [Required]
        [StringLength(75)]
        public string Member_Name { get; set; }

        public int Service_Id { get; set; }

        public int Vehicle_Id { get; set; }

        public int Country_Id { get; set; }

        public int County_Id { get; set; }

        public int Town_Id { get; set; }

        [Required]
        [StringLength(350)]
        public string Address { get; set; }

        [Required]
        [StringLength(25)]
        public string Phone { get; set; }

        [Required]
        [StringLength(25)]
        public string Gsm { get; set; }

        [Required]
        [StringLength(250)]
        public string Email { get; set; }

        public int Borrow { get; set; }

        public decimal Km { get; set; }

        [Required]
        [StringLength(50)]
        public string Member_Title { get; set; }

        [Required]
        [StringLength(11)]
        public string Member_TC { get; set; }

        public virtual Companies Companies { get; set; }

        public virtual Counties Counties { get; set; }

        public virtual Countries Countries { get; set; }

        public virtual Member_Groups Member_Groups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Member_Payments> Member_Payments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Members_Pay_Info> Members_Pay_Info { get; set; }

        public virtual Town Town { get; set; }
    }
}
