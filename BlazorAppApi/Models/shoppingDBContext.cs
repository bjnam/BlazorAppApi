using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BlazorAppApi.Models
{
    public partial class shoppingDBContext : DbContext
    {
        public shoppingDBContext()
        {
        }

        public shoppingDBContext(DbContextOptions<shoppingDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Table> Table { get; set; }
        public virtual DbSet<UvMember> UvMember { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=NBJNOTEBOOK\\SQLEXPRESS;Initial Catalog=shoppingDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UvMember>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("uvMEMBER");

                entity.Property(e => e.Addr).HasMaxLength(30);

                entity.Property(e => e.MemberName)
                    .IsRequired()
                    .HasColumnName("memberName")
                    .HasMaxLength(5)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
