namespace Application.Dtos.Tables;

public class TableCreateDto
{
    public string Name { get; set; }
    public int NumberOfPeople { get; set; }
    public bool IsOccupied { get; set; }
}
