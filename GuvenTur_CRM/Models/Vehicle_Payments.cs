namespace GuvenTur_CRM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vehicle_Payments
    {
        public int Id { get; set; }

        public int Vehicle_Id { get; set; }

        public int Payment { get; set; }

        public int? Payment_Type_Id { get; set; }

        public DateTime? Payment_Date { get; set; }

        public virtual Payment_Types Payment_Types { get; set; }

        public virtual Vehicles Vehicles { get; set; }
    }
}
