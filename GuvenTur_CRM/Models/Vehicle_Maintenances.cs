namespace GuvenTur_CRM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vehicle_Maintenances
    {
        public int Id { get; set; }

        public int Vehicle_Id { get; set; }

        public int Service_Id { get; set; }

        public int Vehicle_Km { get; set; }

        public int Maintenance_Type_Id { get; set; }

        public DateTime Maintenance_Date { get; set; }

        public int Maintenance_Payment { get; set; }

        public virtual Companies Companies { get; set; }

        public virtual Companies Companies1 { get; set; }

        public virtual Maintenance_Types Maintenance_Types { get; set; }

        public virtual Vehicles Vehicles { get; set; }
    }
}
