using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Perpustakaan.Models
{
    public partial class PERPUSTAKAAN_PAWContext : IdentityDbContext
    {
        public PERPUSTAKAAN_PAWContext()
        {
        }

        public PERPUSTAKAAN_PAWContext(DbContextOptions<PERPUSTAKAAN_PAWContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Buku> Buku { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<KatalogBuku> KatalogBuku { get; set; }
        public virtual DbSet<KondisiBuku> KondisiBuku { get; set; }
        public virtual DbSet<Mahasiswa> Mahasiswa { get; set; }
        public virtual DbSet<Peminjaman> Peminjaman { get; set; }
        public virtual DbSet<Pengembalian> Pengembalian { get; set; }
        public virtual DbSet<Rak> Rak { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Buku>(entity =>
            {
                entity.HasKey(e => e.NoBuku);

                entity.Property(e => e.NoBuku).HasColumnName("No_Buku");

                entity.Property(e => e.JudulBuku)
                    .HasColumnName("Judul_Buku")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NoKatalog).HasColumnName("No_Katalog");

                entity.Property(e => e.NoRak).HasColumnName("No_Rak");

                entity.Property(e => e.Penerbit)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pengarang)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.HasOne(d => d.NoKatalogNavigation)
                    .WithMany(p => p.Buku)
                    .HasForeignKey(d => d.NoKatalog)
                    .HasConstraintName("FK_Buku_Katalog_Buku");

                entity.HasOne(d => d.NoRakNavigation)
                    .WithMany(p => p.Buku)
                    .HasForeignKey(d => d.NoRak)
                    .HasConstraintName("FK_Buku_Rak");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.HasKey(e => e.NoGender);

                entity.Property(e => e.NoGender).HasColumnName("No_Gender");

                entity.Property(e => e.NamaGender)
                    .HasColumnName("Nama_Gender")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<KatalogBuku>(entity =>
            {
                entity.HasKey(e => e.NoKatalog);

                entity.ToTable("Katalog_Buku");

                entity.Property(e => e.NoKatalog).HasColumnName("No_Katalog");

                entity.Property(e => e.JenisKatalog)
                    .HasColumnName("Jenis_Katalog")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<KondisiBuku>(entity =>
            {
                entity.HasKey(e => e.NoKondisi);

                entity.ToTable("Kondisi_Buku");

                entity.Property(e => e.NoKondisi).HasColumnName("No_Kondisi");

                entity.Property(e => e.NamaKondisi)
                    .HasColumnName("Nama_Kondisi")
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Mahasiswa>(entity =>
            {
                entity.HasKey(e => e.NoAnggota);

                entity.Property(e => e.NoAnggota).HasColumnName("No_Anggota");

                entity.Property(e => e.Alamat)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Nama)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Nim)
                    .HasColumnName("NIM")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.NoGender).HasColumnName("No_Gender");

                entity.Property(e => e.NoHp)
                    .HasColumnName("No_HP")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.HasOne(d => d.NoGenderNavigation)
                    .WithMany(p => p.Mahasiswa)
                    .HasForeignKey(d => d.NoGender)
                    .HasConstraintName("FK_Mahasiswa_Gender");
            });

            modelBuilder.Entity<Peminjaman>(entity =>
            {
                entity.HasKey(e => e.NoPeminjaman);

                entity.Property(e => e.NoPeminjaman).HasColumnName("No_Peminjaman");

                entity.Property(e => e.NoAnggota).HasColumnName("No_Anggota");

                entity.Property(e => e.NoBuku).HasColumnName("No_Buku");

                entity.Property(e => e.TglPeminjaman)
                    .HasColumnName("Tgl_Peminjaman")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.NoAnggotaNavigation)
                    .WithMany(p => p.Peminjaman)
                    .HasForeignKey(d => d.NoAnggota)
                    .HasConstraintName("FK_Peminjaman_Mahasiswa");

                entity.HasOne(d => d.NoBukuNavigation)
                    .WithMany(p => p.Peminjaman)
                    .HasForeignKey(d => d.NoBuku)
                    .HasConstraintName("FK_Peminjaman_Buku");
            });

            modelBuilder.Entity<Pengembalian>(entity =>
            {
                entity.HasKey(e => e.NoPengembalian);

                entity.Property(e => e.NoPengembalian).HasColumnName("No_Pengembalian");

                entity.Property(e => e.NoKondisi).HasColumnName("No_Kondisi");

                entity.Property(e => e.NoPeminjaman).HasColumnName("No_Peminjaman");

                entity.Property(e => e.TglPengembalian)
                    .HasColumnName("Tgl_Pengembalian")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.NoKondisiNavigation)
                    .WithMany(p => p.Pengembalian)
                    .HasForeignKey(d => d.NoKondisi)
                    .HasConstraintName("FK_Pengembalian_Kondisi_Buku");

                entity.HasOne(d => d.NoPeminjamanNavigation)
                    .WithMany(p => p.Pengembalian)
                    .HasForeignKey(d => d.NoPeminjaman)
                    .HasConstraintName("FK_Pengembalian_Peminjaman");
            });

            modelBuilder.Entity<Rak>(entity =>
            {
                entity.HasKey(e => e.NoRak);

                entity.Property(e => e.NoRak).HasColumnName("No_Rak");

                entity.Property(e => e.NamaRak)
                    .HasColumnName("Nama_Rak")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
