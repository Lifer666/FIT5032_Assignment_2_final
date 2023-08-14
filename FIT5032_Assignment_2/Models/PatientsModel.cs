using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FIT5032_Assignment_2.Models
{
    public partial class PatientsModel : DbContext
    {
        public PatientsModel()
            : base("name=PatientsModel")
        {
        }

        public virtual DbSet<Patients> Patients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patients>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Patients>()
                .Property(e => e.LastName)
                .IsUnicode(false);
        }
    }
}
