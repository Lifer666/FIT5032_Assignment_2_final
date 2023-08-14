using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FIT5032_Assignment_2.Models
{
    public partial class AppointmentsModel : DbContext
    {
        public AppointmentsModel()
            : base("name=AppointmentsModel")
        {
        }

        public virtual DbSet<Appointments> Appointments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
