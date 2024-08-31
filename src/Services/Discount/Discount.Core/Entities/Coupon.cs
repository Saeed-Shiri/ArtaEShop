﻿

using System.ComponentModel.DataAnnotations;

namespace Discount.Core.Entities;
public class Coupon
{
    [Key]
    public int Id { get; set; }
    public string Description { get; set; }
    public string ProductName { get; set; }
    public int Amount { get; set; }
}