﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BusinessObj.Models;

[PrimaryKey("FeedbackId", "ProductId", "UserId")]
[Table("RefFeedback")]
public partial class RefFeedback
{
    [Key]
    [Column("FeedbackID")]
    [StringLength(20)]
    public string FeedbackId { get; set; }

    [Key]
    [Column("ProductID")]
    [StringLength(20)]
    public string ProductId { get; set; }

    [Key]
    [Column("UserID")]
    [StringLength(20)]
    public string UserId { get; set; }

    public bool? Status { get; set; }

    [ForeignKey("FeedbackId")]
    [InverseProperty("RefFeedbacks")]
    public virtual Feedback Feedback { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("RefFeedbacks")]
    public virtual Product Product { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("RefFeedbacks")]
    public virtual User User { get; set; }
}