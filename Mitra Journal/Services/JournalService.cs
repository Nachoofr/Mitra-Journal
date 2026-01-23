using Microsoft.EntityFrameworkCore;
using Mitra_Journal.Data;
using Mitra_Journal.Entities;
using Mitra_Journal.Services.Interface;

namespace Mitra_Journal.Services;

public class JournalService : IJournalService
{
    private readonly LocalDbContext dbConfig;
 
    public JournalService(LocalDbContext dbConfig)
    {
        this.dbConfig = dbConfig;
    }
 
    // Get all journals including related Mood and Tags
    public async Task<(int,List<Journal>)> GetAllJournalsAsync(int pageNo = 1, int pageSize = 3)
    {
        var totalJournalCount = await dbConfig.Journals.CountAsync();
        var paginatedJournals = await dbConfig.Journals
            .Include(j => j.Mood)
            .Include(j => j.Tags)
            .OrderByDescending(j => j.CreatedAt)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (totalJournalCount, paginatedJournals);
    }
 
    // Get a single journal by ID including related Mood and Tags
    public async Task<Journal> GetJournalByIdAsync(Guid id)
    {
        return await dbConfig.Journals
            .Include(j => j.Mood)
            .Include(j => j.Tags)
            .FirstOrDefaultAsync(j => j.JournalId == id);
    }
 
    // Add a new journal
    public async Task AddJournalAsync(Journal journal)
    {
        journal.JournalId = Guid.NewGuid();          // ensure ID
        journal.CreatedAt = DateTime.Now;           // timestamp
        dbConfig.Journals.Add(journal);
        await dbConfig.SaveChangesAsync();
    }
 
    // Update an existing journal
    public async Task UpdateJournalAsync(Journal journal)
    {
        var existingJournal = await dbConfig.Journals.FindAsync(journal.JournalId);
        if (existingJournal != null)
        {
            existingJournal.Title = journal.Title;
            existingJournal.Description = journal.Description;
            existingJournal.Password = journal.Password;
            existingJournal.MoodId = journal.MoodId;
            existingJournal.TagId = journal.TagId;
 
            dbConfig.Journals.Update(existingJournal);
            await dbConfig.SaveChangesAsync();
        }
    }
 
    // Delete a journal by ID
    public async Task DeleteJournalAsync(Guid id)
    {
        var journal = await dbConfig.Journals.FindAsync(id);
        if (journal != null)
        {
            dbConfig.Journals.Remove(journal);
            await dbConfig.SaveChangesAsync();
        }
    }
    public async Task<StreakResult> GetStreaksAsync()
    {
        var totalEntries =  dbConfig.Journals.Count();
        // Get all journal dates (distinct) in ascending order
        var entries = await dbConfig.Journals
            .Select(e => e.CreatedAt.Date)
            .Distinct()
            .OrderBy(d => d)
            .ToListAsync();

        if (!entries.Any())
            return new StreakResult { CurrentStreak = 0, LongestStreak = 0 };
        
        int currentStreak = 0;
        var today = DateTime.Today.Date;
        var checkDate = today;
        
        var entrySet = entries.ToHashSet();

        while (entrySet.Contains(checkDate))
        {
            currentStreak++;
            checkDate = checkDate.AddDays(-1);
        }
        
        int longestStreak = 0;
        int tempStreak = 1;

        for (int i = 1; i < entries.Count; i++)
        {
          
            if ((entries[i] - entries[i - 1]).Days == 1)
            {
                tempStreak++;
            }
            else
            {
                tempStreak = 1;
            }

            if (tempStreak > longestStreak)
                longestStreak = tempStreak;
        }

        if (longestStreak == 0)
            longestStreak = 1;

        return new StreakResult
        {
            TotalJournals =  totalEntries,
            CurrentStreak = currentStreak,
            LongestStreak = longestStreak
        };
    }
}