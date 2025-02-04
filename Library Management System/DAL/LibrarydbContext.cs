﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Library_Management_System.Entities;
using Microsoft.Data.Sqlite;


namespace Library_Management_System.DAL;

public partial class LibrarydbContext : DbContext
{
    public LibrarydbContext()
    {
    }

    public LibrarydbContext(DbContextOptions<LibrarydbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AudioBook> AudioBooks { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Borrower> Borrowers { get; set; }

    public virtual DbSet<Dvd> Dvds { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<LibraryMedia> LibraryMedia { get; set; }

    public virtual DbSet<LibraryTransaction> LibraryTransactions { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlite("Data Source=\"D:\\School\\Bow Valley College\\Second Term\\SODV1254Object Oriented Programming Concepts\\Assignment-1\\library.db\"");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AudioBook>(entity =>
        {
            entity.Property(e => e.AudioBookId).ValueGeneratedNever();

            entity.HasOne(d => d.AudioBookNavigation).WithOne(p => p.AudioBook).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.Property(e => e.BookId).ValueGeneratedNever();

            entity.HasOne(d => d.BookNavigation).WithOne(p => p.Book).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Dvd>(entity =>
        {
            entity.Property(e => e.Dvdid).ValueGeneratedNever();

            entity.HasOne(d => d.DvdNavigation).WithOne(p => p.Dvd).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<LibraryMedia>(entity =>
        {
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
        });

        modelBuilder.Entity<LibraryTransaction>(entity =>
        {
            entity.HasOne(d => d.Borrower).WithMany(p => p.LibraryTransactions).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Media).WithMany(p => p.LibraryTransactions).OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}