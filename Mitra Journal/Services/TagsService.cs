using Microsoft.EntityFrameworkCore;
using Mitra_Journal.Data;
using Mitra_Journal.Entities;
using Mitra_Journal.Services.Interface;

namespace Mitra_Journal.Services;

public class TagsService : ITagsService
{
    private LocalDbContext _dbContext;

    public TagsService(LocalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Tags> GetTags()
    {
        return _dbContext.Tags.ToList();
    }

    public Tags AddTag(string tagName)
    {
        // Check if already exists
        var existing = _dbContext.Tags.FirstOrDefault(t => t.TagName.ToLower() == tagName.ToLower());
        if (existing != null)
            return existing;

        var tag = new Tags
        {
            TagId = Guid.NewGuid(),
            TagName = tagName
        };

        _dbContext.Tags.Add(tag);
        _dbContext.SaveChanges();

        return tag;
    }

    public Dictionary<string, int> GetTagsAnalytics()
    {
        return _dbContext.JournalTags
            .Include(jt => jt.Tag)
            .GroupBy(jt => jt.Tag.TagName)
            .ToDictionary(
                g => g.Key,
                g => g.Count()
            );
    }
}