﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BusinessObj.Models;

[Table("Attribute")]
public partial class Attribute
{
    [Key]
    [Column("AttributeID")]
    [StringLength(20)]
    public string AttributeId { get; set; }

    public int Type { get; set; }

    [Required]
    [StringLength(30)]
    public string Name { get; set; }

    [Column("Img_url")]
    public string ImgUrl { get; set; }

    public double Price { get; set; }

    public int Quantity { get; set; }

    [Required]
    [StringLength(150)]
    public string Desription { get; set; }

    [Column("Create_by")]
    [StringLength(20)]
    public string CreateBy { get; set; }

    [Column("Create_at", TypeName = "datetime")]
    public DateTime? CreateAt { get; set; }

    [Column("Update_by")]
    [StringLength(20)]
    public string UpdateBy { get; set; }

    [Column("Update_at", TypeName = "datetime")]
    public DateTime? UpdateAt { get; set; }

    [Column("Delete_by")]
    [StringLength(20)]
    public string DeleteBy { get; set; }

    [Column("Delete_at", TypeName = "datetime")]
    public DateTime? DeleteAt { get; set; }

    public bool? Status { get; set; }

    [InverseProperty("Attribute")]
    public virtual ICollection<RefProductAttribute> RefProductAttributes { get; set; } = new List<RefProductAttribute>();
}