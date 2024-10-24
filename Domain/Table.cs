﻿namespace Domain;

public class Table
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int NumberOfPeople { get; set; }
    public bool IsOccupied { get; set; }

    public List<Guid> OrderIds { get; set; }
    public IEnumerable<Order> Orders { get; set; }
}
