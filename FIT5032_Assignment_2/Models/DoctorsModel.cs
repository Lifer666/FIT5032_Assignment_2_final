using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FIT5032_Assignment_2.Models
{
    public partial class DoctorsModel : DbContext
    {
        public DoctorsModel()
            : base("name=DoctorsModel")
        {
        }

        public virtual DbSet<Doctors> Doctors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
