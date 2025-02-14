﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Entities;

[Table("LibraryTransaction")]
public partial class LibraryTransaction
{
    [Key]
    public int TransactionId { get; set; }

    public int BorrowerId { get; set; }

    public int MediaId { get; set; }

    [Column(TypeName = "DATE")]
    public DateOnly BorrowDate { get; set; }

    [Column(TypeName = "DATE")]
    public DateOnly? ReturnDate { get; set; }

    [ForeignKey("BorrowerId")]
    [InverseProperty("LibraryTransactions")]
    public virtual Borrower Borrower { get; set; }

    [ForeignKey("MediaId")]
    [InverseProperty("LibraryTransactions")]
    public virtual MediaLibrary Media { get; set; }
}