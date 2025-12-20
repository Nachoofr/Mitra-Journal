using System.ComponentModel.DataAnnotations;

namespace Mitra_Journal.Entities;

public class Tags
{
    [Key]
    public Guid  TagId { get; set; }
    
    public required string TagName { get; set; }
    
}