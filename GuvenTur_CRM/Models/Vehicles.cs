namespace GuvenTur_CRM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vehicles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehicles()
        {
            Vehicle_Files = new HashSet<Vehicle_Files>();
            Vehicle_Maintenances = new HashSet<Vehicle_Maintenances>();
            Vehicle_Payments = new HashSet<Vehicle_Payments>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(11)]
        public string Driver_TC { get; set; }

        [Required]
        [StringLength(50)]
        public string Driver_Name { get; set; }

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

        [StringLength(350)]
        public string Email { get; set; }

        public int Salary { get; set; }

        public int Vehicle_No { get; set; }

        [Required]
        [StringLength(50)]
        public string Vehicle_Plate { get; set; }

        public int Service_Id { get; set; }

        public virtual Companies Companies { get; set; }

        public virtual Counties Counties { get; set; }

        public virtual Countries Countries { get; set; }

        public virtual Town Town { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vehicle_Files> Vehicle_Files { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vehicle_Maintenances> Vehicle_Maintenances { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vehicle_Payments> Vehicle_Payments { get; set; }
    }
}
