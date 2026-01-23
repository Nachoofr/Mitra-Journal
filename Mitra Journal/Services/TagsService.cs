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
}