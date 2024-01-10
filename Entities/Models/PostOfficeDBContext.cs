using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entities.Models
{
    public partial class PostOfficeDBContext : DbContext
    {
        public PostOfficeDBContext()
        {
        }

        public PostOfficeDBContext(DbContextOptions<PostOfficeDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblHistory> TblHistories { get; set; } = null!;
        public virtual DbSet<TblPackage> TblPackages { get; set; } = null!;
        public virtual DbSet<TblPostOffice> TblPostOffices { get; set; } = null!;
        public virtual DbSet<TblShipment> TblShipments { get; set; } = null!;
        public virtual DbSet<TblTransportation> TblTransportations { get; set; } = null!;
        public virtual DbSet<TblUser> TblUsers { get; set; } = null!;
        public virtual DbSet<TblUserType> TblUserTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=MARINELA;Database=PostOfficeDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId)
                    .HasName("PK__tblHisto__4D7B4ABD0EDF9108");

                entity.ToTable("tblHistory");

                entity.Property(e => e.HistoryId).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblPackage>(entity =>
            {
                entity.HasKey(e => e.PackageId)
                    .HasName("PK__tblPacka__322035ECB7363C6F");

                entity.ToTable("tblPackage");

                entity.Property(e => e.PackageId)
                    .ValueGeneratedNever()
                    .HasColumnName("PackageID");

                entity.Property(e => e.Barcode).IsUnicode(false);

                entity.Property(e => e.DestinationPostOfficeId).HasColumnName("DestinationPostOfficeID");

                entity.Property(e => e.SourcePostOfficeId).HasColumnName("SourcePostOfficeID");

                entity.HasOne(d => d.DestinationPostOffice)
                    .WithMany(p => p.TblPackageDestinationPostOffices)
                    .HasForeignKey(d => d.DestinationPostOfficeId)
                    .HasConstraintName("FK__tblPackag__PostO__5EBF139D");

                entity.HasOne(d => d.SourcePostOffice)
                    .WithMany(p => p.TblPackageSourcePostOffices)
                    .HasForeignKey(d => d.SourcePostOfficeId)
                    .HasConstraintName("FK__tblPackag__Sourc__60A75C0F");

                entity.HasOne(d => d.UserCreated)
                    .WithMany(p => p.TblPackages)
                    .HasForeignKey(d => d.UserCreatedId)
                    .HasConstraintName("FK__tblPackag__UserC__5DCAEF64");
            });

            modelBuilder.Entity<TblPostOffice>(entity =>
            {
                entity.HasKey(e => e.PostOfficeId);

                entity.ToTable("tblPostOffice");

                entity.HasIndex(e => e.Name, "UQ__tblPostO__737584F6A005118E")
                    .IsUnique();

                entity.Property(e => e.PostOfficeId)
                    .ValueGeneratedNever()
                    .HasColumnName("PostOfficeID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblShipment>(entity =>
            {
                entity.HasKey(e => e.ShipmentId);

                entity.ToTable("tblShipments");

                entity.Property(e => e.ShipmentId)
                    .ValueGeneratedNever()
                    .HasColumnName("ShipmentID");

                entity.Property(e => e.Barcode).IsUnicode(false);

                entity.Property(e => e.DeliverPlace)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PackageId).HasColumnName("PackageID");

                entity.Property(e => e.ReceiverUserDetails).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.TblShipmentCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__tblShipme__Creat__52593CB8");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.TblShipments)
                    .HasForeignKey(d => d.PackageId)
                    .HasConstraintName("FK__tblShipme__PostO__5AEE82B9");

                entity.HasOne(d => d.SendUser)
                    .WithMany(p => p.TblShipmentSendUsers)
                    .HasForeignKey(d => d.SendUserId)
                    .HasConstraintName("FK__tblShipme__SendU__2E1BDC42");
            });

            modelBuilder.Entity<TblTransportation>(entity =>
            {
                entity.HasKey(e => e.TransportationId)
                    .HasName("PK__tblTrans__87E4795633C5F37C");

                entity.ToTable("tblTransportations");

                entity.Property(e => e.TransportationId)
                    .ValueGeneratedNever()
                    .HasColumnName("TransportationID");

                entity.Property(e => e.ShipmentId).HasColumnName("ShipmentID");

                entity.Property(e => e.TransporterUserId).HasColumnName("TransporterUserID");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.TblTransportations)
                    .HasForeignKey(d => d.PackageId)
                    .HasConstraintName("FK__tblTransp__Packa__619B8048");

                entity.HasOne(d => d.Shipment)
                    .WithMany(p => p.TblTransportations)
                    .HasForeignKey(d => d.ShipmentId)
                    .HasConstraintName("FK__tblTransp__Shipm__32E0915F");

                entity.HasOne(d => d.TransporterUser)
                    .WithMany(p => p.TblTransportations)
                    .HasForeignKey(d => d.TransporterUserId)
                    .HasConstraintName("FK__tblTransp__Trans__30F848ED");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__tblUsers__1788CC4C19B3AB59");

                entity.ToTable("tblUsers");

                entity.HasIndex(e => e.Username, "UQ__tblUsers__536C85E4EE009A09")
                    .IsUnique();

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.TblUsers)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblUsers__UserTy__3A81B327");
            });

            modelBuilder.Entity<TblUserType>(entity =>
            {
                entity.HasKey(e => e.UserTypeId)
                    .HasName("PK__tblUserT__40D2D816070BE135");

                entity.ToTable("tblUserType");

                entity.Property(e => e.UserTypeId).ValueGeneratedNever();

                entity.Property(e => e.IsActive)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
