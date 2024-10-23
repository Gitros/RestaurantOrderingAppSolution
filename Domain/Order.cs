using System;
using System.Collections.Generic;

namespace Domain;

public class Order
{
    public int Id { get; set; }
    public string Table { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public decimal TotalAmount => OrderItems.Sum(item => item.Price * item.Quantity);
}
