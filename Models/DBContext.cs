namespace TesteS2IT.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;

    public partial class DBContext : IdentityDbContext<AppUser>
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<Friend> Friend { get; set; }
        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<Lending> Lending { get; set; }

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

            modelBuilder.Entity<Games>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Games>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Games>()
                .Property(e => e.Year)
                .IsFixedLength();

            modelBuilder.Entity<Games>()
                .Property(e => e.UrlImage)
                .IsUnicode(false);

            modelBuilder.Entity<Lending>()
                .HasKey(e => e.ID);

            modelBuilder.Entity<Lending>()
                .Property(e => e.DateLended)
                .IsOptional();

            modelBuilder.Entity<Lending>()
            .Property(e => e.DateReturned)
            .IsOptional();

            modelBuilder.Entity<IdentityUserLogin>()
            .HasKey<string>(l => l.UserId);

            modelBuilder.Entity<IdentityRole>()
            .HasKey<string>(r => r.Id);

            modelBuilder.Entity<IdentityUserRole>()
            .HasKey(r => new { r.RoleId, r.UserId });

        }
    }
}
