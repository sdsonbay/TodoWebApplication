namespace TodoWebApplication.Dtos;

public class TodoDto
{
    public int Id { get; set; }
    public double Progress { get; set; }
    public bool IsDone { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }
}