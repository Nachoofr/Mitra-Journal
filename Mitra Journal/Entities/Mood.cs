using System.ComponentModel.DataAnnotations;

namespace Mitra_Journal.Entities;

public class Mood
{
    [Key]
    public Guid MoodId { get; set; }
    
    public string MoodName { get; set; }
}