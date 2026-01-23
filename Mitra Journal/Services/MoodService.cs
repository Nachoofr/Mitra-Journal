using Microsoft.EntityFrameworkCore;
using Mitra_Journal.Data;
using Mitra_Journal.Entities;

namespace Mitra_Journal.Services;

public class MoodService : IMoodService
{
    private LocalDbContext _dbContext;

    public MoodService(LocalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Mood> GetAllMoods()
    {
        return _dbContext.Moods.ToList();
    }

    public Dictionary<string, int> GetMoodAnalytics()
    {
        return _dbContext.Journals
            .Include(j => j.Mood)
            .GroupBy(j => j.Mood.MoodName)
            .ToDictionary(
                g => g.Key,
                g => g.Count()
            );
    }
}