namespace GuvenTur_CRM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Member_Payments
    {
        public int Id { get; set; }

        public int Member_Id { get; set; }

        public int Payment { get; set; }

        public int? Payment_Type_Id { get; set; }

        public DateTime? Payment_Date { get; set; }

        public virtual Members Members { get; set; }

        public virtual Payment_Types Payment_Types { get; set; }
    }
}
