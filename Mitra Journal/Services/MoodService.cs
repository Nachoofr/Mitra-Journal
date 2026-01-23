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
        return  _dbContext.Moods.ToList();
    }
}