namespace Application.Dtos.Tables;

public class TableReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int NumberOfPeople { get; set; }
    public bool IsOccupied { get; set; }
}
