﻿namespace Application.Dtos.Reservations;

public class ReservationReadDto
{
    public Guid Id { get; set; }
    public string PhoneNumber { get; set; }
    public string Surname { get; set; }
    public DateTime ReservationDateTime { get; set; }
    public int NumberOfPeople { get; set; }
    public bool IsAssigned { get; set; }
    public string? TableName { get; set; }
}
