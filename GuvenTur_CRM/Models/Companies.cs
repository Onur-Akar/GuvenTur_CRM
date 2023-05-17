namespace GuvenTur_CRM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Companies
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Companies()
        {
            Members = new HashSet<Members>();
            Vehicle_Maintenances = new HashSet<Vehicle_Maintenances>();
            Vehicle_Maintenances1 = new HashSet<Vehicle_Maintenances>();
            Vehicles = new HashSet<Vehicles>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Service_Name { get; set; }

        public int Country_Id { get; set; }

        public int County_Id { get; set; }

        public int Town_Id { get; set; }

        [Required]
        [StringLength(350)]
        public string Address { get; set; }

        [Required]
        [StringLength(25)]
        public string Phone { get; set; }

        [StringLength(25)]
        public string Fax { get; set; }

        [Required]
        [StringLength(350)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Authorized_Name { get; set; }

        [Required]
        [StringLength(25)]
        public string Authorized_Phone { get; set; }

        [Required]
        [StringLength(350)]
        public string Authorized_Email { get; set; }

        [Required]
        [StringLength(90)]
        public string Company_Title { get; set; }

        [Required]
        [StringLength(150)]
        public string Tax_Office { get; set; }

        [Required]
        [StringLength(15)]
        public string Tax_Number { get; set; }

        public virtual Counties Counties { get; set; }

        public virtual Countries Countries { get; set; }

        public virtual Town Town { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Members> Members { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vehicle_Maintenances> Vehicle_Maintenances { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vehicle_Maintenances> Vehicle_Maintenances1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vehicles> Vehicles { get; set; }
    }
}
