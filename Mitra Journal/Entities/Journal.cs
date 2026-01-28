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
    
    public Guid MoodId { get; set; }
    public Guid? SecondaryMoodId1 { get; set; }
    public Guid? SecondaryMoodId2 { get; set; }
    [ForeignKey("MoodId")]
    
    public Mood Mood{ get; set; }
    
    public ICollection<JournalTags> JournalTags { get; set; }
}