using System;
using System.Collections.Generic;
using System.Data;

namespace Dynasy.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public int BuyerId { get; set; }
    public string BuyerName { get; set; }
    public int SellerId { get; set; }
    public string SellerName { get; set; }
    public List<ProductDto> Products { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateOrderDto
{
    public int BuyerId { get; set; }
    public List<int> ProductIds { get; set; }
}

public class UpdateOrderDto
{
    public OrderStatus Status { get; set; }
}