namespace GuvenTur_CRM.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class GuvenTurDBModel : DbContext
    {
        public GuvenTurDBModel()
            : base("name=GuvenTurDALModel")
        {
        }

        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<Counties> Counties { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Level_Settings> Level_Settings { get; set; }
        public virtual DbSet<Levels> Levels { get; set; }
        public virtual DbSet<Maintenance_Types> Maintenance_Types { get; set; }
        public virtual DbSet<Member_Groups> Member_Groups { get; set; }
        public virtual DbSet<Member_Payments> Member_Payments { get; set; }
        public virtual DbSet<Members> Members { get; set; }
        public virtual DbSet<Members_Pay_Info> Members_Pay_Info { get; set; }
        public virtual DbSet<Payment_Types> Payment_Types { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Town> Town { get; set; }
        public virtual DbSet<User_Special_Settings> User_Special_Settings { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Vehicle_Files> Vehicle_Files { get; set; }
        public virtual DbSet<Vehicle_Maintenances> Vehicle_Maintenances { get; set; }
        public virtual DbSet<Vehicle_Payments> Vehicle_Payments { get; set; }
        public virtual DbSet<Vehicles> Vehicles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Companies>()
                .HasMany(e => e.Members)
                .WithRequired(e => e.Companies)
                .HasForeignKey(e => e.Service_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Companies>()
                .HasMany(e => e.Vehicle_Maintenances)
                .WithRequired(e => e.Companies)
                .HasForeignKey(e => e.Service_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Companies>()
                .HasMany(e => e.Vehicle_Maintenances1)
                .WithRequired(e => e.Companies1)
                .HasForeignKey(e => e.Service_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Companies>()
                .HasMany(e => e.Vehicles)
                .WithRequired(e => e.Companies)
                .HasForeignKey(e => e.Service_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Counties>()
                .Property(e => e.County_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Counties>()
                .HasMany(e => e.Companies)
                .WithRequired(e => e.Counties)
                .HasForeignKey(e => e.County_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Counties>()
                .HasMany(e => e.Members)
                .WithRequired(e => e.Counties)
                .HasForeignKey(e => e.County_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Counties>()
                .HasMany(e => e.Town)
                .WithOptional(e => e.Counties)
                .HasForeignKey(e => e.County_Id);

            modelBuilder.Entity<Counties>()
                .HasMany(e => e.Vehicles)
                .WithRequired(e => e.Counties)
                .HasForeignKey(e => e.County_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Countries>()
                .Property(e => e.Country_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Countries>()
                .HasMany(e => e.Companies)
                .WithRequired(e => e.Countries)
                .HasForeignKey(e => e.Country_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Countries>()
                .HasMany(e => e.Counties)
                .WithOptional(e => e.Countries)
                .HasForeignKey(e => e.Country_Id);

            modelBuilder.Entity<Countries>()
                .HasMany(e => e.Members)
                .WithRequired(e => e.Countries)
                .HasForeignKey(e => e.Country_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Countries>()
                .HasMany(e => e.Vehicles)
                .WithRequired(e => e.Countries)
                .HasForeignKey(e => e.Country_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Levels>()
                .HasMany(e => e.Level_Settings)
                .WithRequired(e => e.Levels)
                .HasForeignKey(e => e.Level_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Levels>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Levels)
                .HasForeignKey(e => e.Level_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Maintenance_Types>()
                .HasMany(e => e.Vehicle_Maintenances)
                .WithRequired(e => e.Maintenance_Types)
                .HasForeignKey(e => e.Maintenance_Type_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member_Groups>()
                .HasMany(e => e.Members)
                .WithRequired(e => e.Member_Groups)
                .HasForeignKey(e => e.Member_Group_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Members>()
                .Property(e => e.Km)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Members>()
                .HasMany(e => e.Member_Payments)
                .WithRequired(e => e.Members)
                .HasForeignKey(e => e.Member_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Members>()
                .HasMany(e => e.Members_Pay_Info)
                .WithRequired(e => e.Members)
                .HasForeignKey(e => e.Member_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Payment_Types>()
                .HasMany(e => e.Member_Payments)
                .WithOptional(e => e.Payment_Types)
                .HasForeignKey(e => e.Payment_Type_Id);

            modelBuilder.Entity<Payment_Types>()
                .HasMany(e => e.Vehicle_Payments)
                .WithOptional(e => e.Payment_Types)
                .HasForeignKey(e => e.Payment_Type_Id);

            modelBuilder.Entity<Town>()
                .Property(e => e.Town_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Town>()
                .HasMany(e => e.Companies)
                .WithRequired(e => e.Town)
                .HasForeignKey(e => e.Town_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Town>()
                .HasMany(e => e.Members)
                .WithRequired(e => e.Town)
                .HasForeignKey(e => e.Town_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Town>()
                .HasMany(e => e.Vehicles)
                .WithRequired(e => e.Town)
                .HasForeignKey(e => e.Town_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.User_Special_Settings)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.User_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehicle_Files>()
                .Property(e => e.First_Km_Maintenance)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Vehicle_Files>()
                .Property(e => e.Next_Km_Maintenance)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Vehicles>()
                .HasMany(e => e.Vehicle_Files)
                .WithRequired(e => e.Vehicles)
                .HasForeignKey(e => e.Vehicle_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehicles>()
                .HasMany(e => e.Vehicle_Maintenances)
                .WithRequired(e => e.Vehicles)
                .HasForeignKey(e => e.Vehicle_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehicles>()
                .HasMany(e => e.Vehicle_Payments)
                .WithRequired(e => e.Vehicles)
                .HasForeignKey(e => e.Vehicle_Id)
                .WillCascadeOnDelete(false);
        }
    }
}
