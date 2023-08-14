using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FIT5032_Assignment_2.Models
{
    public partial class AdministratorsModel : DbContext
    {
        public AdministratorsModel()
            : base("name=AdministratorsModel")
        {
        }

        public virtual DbSet<Administrators> Administrators { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        //public System.Data.Entity.DbSet<FIT5032_Assignment_2.Models.Appointments> Appointments { get; set; }
    }
}
