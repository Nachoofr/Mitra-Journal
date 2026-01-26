namespace Mitra_Journal.Entities;

public class JournalTags
{
    public int JournalTagsId { get; set; }
    public Guid TagId { get; set; }
    public Guid JournalId { get; set; }
    public Journal Journal { get; set; }
    public Tags Tag { get; set; }
}