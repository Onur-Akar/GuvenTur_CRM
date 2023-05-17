namespace GuvenTur_CRM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vehicle_Files
    {
        public int Id { get; set; }

        public int Vehicle_Id { get; set; }

        public decimal First_Km_Maintenance { get; set; }

        public decimal Next_Km_Maintenance { get; set; }

        public DateTime Traffic_Insurance_Date { get; set; }

        public DateTime Examination_Date { get; set; }

        [Required]
        [StringLength(250)]
        public string License_Sample { get; set; }

        [Required]
        [StringLength(250)]
        public string Traffic_Insurance_Sample { get; set; }

        [Required]
        [StringLength(250)]
        public string Examination { get; set; }

        [Required]
        [StringLength(250)]
        public string Driver_License { get; set; }

        [Required]
        [StringLength(250)]
        public string Registry_Record { get; set; }

        [Required]
        [StringLength(250)]
        public string Gbt { get; set; }

        [Required]
        [StringLength(250)]
        public string Health_Report { get; set; }

        public virtual Vehicles Vehicles { get; set; }
    }
}
