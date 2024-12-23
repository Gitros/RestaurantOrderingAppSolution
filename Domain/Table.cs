﻿namespace Domain;

public class Table
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int NumberOfPeople { get; set; }
    public bool IsOccupied { get; set; }

    public bool IsUsed { get; set; }
    public bool IsDeleted { get; set; }

    public List<Order> Orders { get; set; } = new List<Order>();
}
