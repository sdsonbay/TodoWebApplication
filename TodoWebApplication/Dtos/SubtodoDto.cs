namespace TodoWebApplication.Dtos;

public class SubtodoDto
{
    public int Id { get; set; }
    public bool IsDone { get; set; }
    public int todoId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }
}