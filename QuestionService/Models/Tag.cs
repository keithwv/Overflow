using System.ComponentModel.DataAnnotations;

namespace QuestionService.Models;

public class Tag
{
    [MaxLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(50)]
    public string Slug { get; set; }
    [MaxLength(1000)]
    public string Description { get; set; }
}