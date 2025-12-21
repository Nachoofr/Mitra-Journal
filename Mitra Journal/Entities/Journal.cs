using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mitra_Journal.Entities;

public class Journal
{
    [Key]
    public required Guid JournalId { get; set; }
    
    public required string Title { get; set; }
    
    public required string Description { get; set; }
    
    public required DateTime CreatedAt { get; set; }
    
    public string? Password { get; set; }
    
    public Guid MoodId { get; set; }
    [ForeignKey("MoodId")]
    
    public Mood Mood{ get; set; }
    
    public Guid TagId { get; set; }
    [ForeignKey("TagId")]
    
    public Tags Tags { get; set; }
}