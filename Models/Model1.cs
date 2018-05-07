namespace TesteS2IT.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=DBModel")
        {
        }

        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friend>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Friend>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Friend>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Friend>()
                .Property(e => e.Mail)
                .IsUnicode(false);

            modelBuilder.Entity<Game>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Game>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Game>()
                .Property(e => e.Year)
                .IsFixedLength();
        }
    }
}
