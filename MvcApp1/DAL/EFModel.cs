namespace MvcApp1.DAL

{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using MvcApp1.Models;

    public class EFModel : DbContext
    {

        public EFModel()
            : base("name=EFModel")
        {
        }


         public  virtual DbSet<Moviment> Moviments { get; set; }
      
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);
        }
    }

}