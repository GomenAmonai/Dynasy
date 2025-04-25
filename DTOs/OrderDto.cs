using System;
using System.Collections.Generic;
using System.Data;

namespace Dynasy.DTOs;

public class OrderDto {
    public int Id { get; set; }
    public DateTime OrderData { get; set; }
    public decimal TotalAmount { get; set; }
    public int UserId { get; set; }
    public List<ProductDto> Products { get; set; } = new List<ProductDto>();
}