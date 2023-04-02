namespace TodoWebApplication.Models;

public class Subtodo
{
    public int Id { get; set; }
    public bool IsDone { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }
    public int TodoId { get; set; }
    public Todo Todo { get; set; }
}