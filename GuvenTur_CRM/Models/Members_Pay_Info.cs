namespace GuvenTur_CRM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Members_Pay_Info
    {
        public int Id { get; set; }

        public int Member_Id { get; set; }

        [StringLength(75)]
        public string Bank_Name { get; set; }

        [StringLength(75)]
        public string Card_Owner { get; set; }

        [StringLength(19)]
        public string Card_Number { get; set; }

        [StringLength(5)]
        public string Last_Exp_Date { get; set; }

        [StringLength(3)]
        public string Securty_No { get; set; }

        public virtual Members Members { get; set; }
    }
}
