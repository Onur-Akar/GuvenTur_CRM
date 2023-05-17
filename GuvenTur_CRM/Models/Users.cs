namespace GuvenTur_CRM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            User_Special_Settings = new HashSet<User_Special_Settings>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string User_Title { get; set; }

        [Required]
        [StringLength(50)]
        public string User_First_Name { get; set; }

        [Required]
        [StringLength(50)]
        public string User_Last_Name { get; set; }

        [Required]
        [StringLength(15)]
        public string Password { get; set; }

        public int Phone_Line { get; set; }

        [Required]
        [StringLength(25)]
        public string Gsm { get; set; }

        [Required]
        [StringLength(250)]
        public string Email { get; set; }

        [Required]
        [StringLength(350)]
        public string Address { get; set; }

        [Required]
        [StringLength(11)]
        public string User_TC { get; set; }

        public int Level_Id { get; set; }

        public virtual Levels Levels { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User_Special_Settings> User_Special_Settings { get; set; }
    }
}
