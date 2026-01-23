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
        return  _dbContext.Tags.ToList();
    }
    public Dictionary<string, int> GetTagsAnalytics()
    {
        return _dbContext.Journals
            .Include(j => j.Tags)
            .GroupBy(j => j.Tags.TagName)
            .ToDictionary(
                g => g.Key,
                g => g.Count()
            );
    }
}