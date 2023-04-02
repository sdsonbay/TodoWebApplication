using System.ComponentModel.DataAnnotations;

namespace TodoWebApplication.Models;

public class Todo
{
    public int Id { get; set; }
    [Range(0, 100)]
    public double Progress { get; set; }
    public bool IsDone { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public ICollection<Subtodo> Subtodos { get; set; }
}