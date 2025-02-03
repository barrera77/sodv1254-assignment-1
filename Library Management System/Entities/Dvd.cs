﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Entities;

[Table("DVD")]
public partial class Dvd
{
    [Key]
    [Column("DVDId")]
    public int Dvdid { get; set; }

    [Required]
    public string Director { get; set; }

    public int Runtime { get; set; }

    [ForeignKey("Dvdid")]
    [InverseProperty("Dvd")]
    public virtual Medium DvdNavigation { get; set; }
}