using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL.Models;

public partial class Hotel3Context : DbContext
{
    public Hotel3Context()
    {
    }

    public Hotel3Context(DbContextOptions<Hotel3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Booked> Bookeds { get; set; }

    public virtual DbSet<BookedList> BookedLists { get; set; }

    public virtual DbSet<BookedService> BookedServices { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer(GetConnectionString());

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnectionStringDB"];

        return strConn;
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booked>(entity =>
        {
            entity.HasKey(e => e.RoomNumber).HasName("PK__Booked__AE10E07B0EEDE316");

            entity.ToTable("Booked");

            entity.Property(e => e.RoomNumber).ValueGeneratedNever();
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.BookStatus)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.DateCreate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.GuestName)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.RoomNumberNavigation).WithOne(p => p.Booked)
                .HasForeignKey<Booked>(d => d.RoomNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booked__RoomNumb__52593CB8");
        });

        modelBuilder.Entity<BookedList>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__BookedLi__73951ACD06860FDE");

            entity.ToTable("BookedList");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.CheckIn).HasColumnType("datetime");
            entity.Property(e => e.CheckOut).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.GuestName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Services).HasColumnType("text");
            entity.Property(e => e.TotalBill).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<BookedService>(entity =>
        {
            entity.HasKey(e => e.ServiceDetailId).HasName("PK__ServiceD__6F80952CE63C420B");

            entity.ToTable("BookedService");

            entity.Property(e => e.ServiceDetailId).HasColumnName("ServiceDetailID");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.NameService)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.TaskId).HasColumnName("TaskID");

            entity.HasOne(d => d.NameServiceNavigation).WithMany(p => p.BookedServices)
                .HasForeignKey(d => d.NameService)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ServiceDe__NameS__5629CD9C");

            entity.HasOne(d => d.RoomNumberNavigation).WithMany(p => p.BookedServices)
                .HasForeignKey(d => d.RoomNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ServiceDe__RoomN__571DF1D5");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Member__0CF04B38DA193E00");

            entity.ToTable("Member");

            entity.HasIndex(e => e.Email, "UQ__Member__A9D10534B996307D").IsUnique();

            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomNumber).HasName("PK__Room__AE10E07BAC55D09E");

            entity.ToTable("Room");

            entity.Property(e => e.RoomNumber).ValueGeneratedNever();
            entity.Property(e => e.RoomStatus)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.NameService).HasName("PK__Service__63F0CE4AE926C9A7");

            entity.ToTable("Service");

            entity.Property(e => e.NameService)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
