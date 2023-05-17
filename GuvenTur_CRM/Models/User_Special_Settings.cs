namespace GuvenTur_CRM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_Special_Settings
    {
        public int Id { get; set; }

        public int User_Id { get; set; }

        public bool? Add_Company { get; set; }

        public bool? Edit_Company { get; set; }

        public bool? Delete_Company { get; set; }

        public bool? See_Comp_Accounts { get; set; }

        public bool? Add_Vehicle { get; set; }

        public bool? Edit_Vehicle { get; set; }

        public bool? Delete_Vehicle { get; set; }

        public bool? See_Vehicle_Accounts { get; set; }

        public bool? Add_Member { get; set; }

        public bool? Edit_Member { get; set; }

        public bool? Delete_Member { get; set; }

        public bool? See_Member_Accounts { get; set; }

        public virtual Users Users { get; set; }
    }
}
