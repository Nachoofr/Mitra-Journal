using Microsoft.EntityFrameworkCore;
using Mitra_Journal.Data;
using Mitra_Journal.Entities;
using Mitra_Journal.Services.Interface;

namespace Mitra_Journal.Services;

public class JournalService : IJournalService
{
    private readonly LocalDbContext _db;

    public JournalService(LocalDbContext db)
    {
        _db = db;
    }

public async Task<(int TotalCount, List<Journal> Journals)> GetAllJournalsAsync(int pageNo = 1, int pageSize = 3)
{
    var totalCount = await _db.Journals.CountAsync();

    var journals = await _db.Journals
        .Include(j => j.Mood)
        .Include(j => j.JournalTags)
            .ThenInclude(jt => jt.Tag)
        .OrderByDescending(j => j.CreatedAt)
        .Skip((pageNo - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    return (totalCount, journals);
}

public async Task<Journal?> GetJournalByIdAsync(Guid id)
{
    return await _db.Journals
        .Include(j => j.Mood)
        .Include(j => j.JournalTags)
            .ThenInclude(jt => jt.Tag)
        .FirstOrDefaultAsync(j => j.JournalId == id);
}

public async Task AddJournalAsync(Journal journal)
{
    journal.JournalId = Guid.NewGuid();
    journal.CreatedAt = DateTime.Now;

    _db.Journals.Add(journal);

    if (journal.JournalTags != null && journal.JournalTags.Any())
    {
        foreach (var jt in journal.JournalTags)
        {
            jt.JournalId = journal.JournalId;
        }
        _db.JournalTags.AddRange(journal.JournalTags);
    }

    await _db.SaveChangesAsync();
}

public async Task UpdateJournalAsync(Journal journal)
{
    var existing = await _db.Journals
        .Include(j => j.JournalTags)
        .FirstOrDefaultAsync(j => j.JournalId == journal.JournalId);

    if (existing == null) return;

    existing.Title = journal.Title;
    existing.Description = journal.Description;
    existing.MoodId = journal.MoodId;

    // Remove old tags
    _db.JournalTags.RemoveRange(existing.JournalTags);

    // Add updated tags
    if (journal.JournalTags != null && journal.JournalTags.Any())
    {
        foreach (var jt in journal.JournalTags)
        {
            jt.JournalId = existing.JournalId;
        }
        _db.JournalTags.AddRange(journal.JournalTags);
    }

    await _db.SaveChangesAsync();
}

public async Task DeleteJournalAsync(Guid id)
{
    var journal = await _db.Journals
        .Include(j => j.JournalTags)
        .FirstOrDefaultAsync(j => j.JournalId == id);

    if (journal == null) return;

    _db.JournalTags.RemoveRange(journal.JournalTags);
    _db.Journals.Remove(journal);

    await _db.SaveChangesAsync();
}

public async Task<StreakResult> GetStreaksAsync()
{
    var totalEntries = await _db.Journals.CountAsync();

    var dates = await _db.Journals
        .Select(j => j.CreatedAt.Date)
        .Distinct()
        .OrderBy(d => d)
        .ToListAsync();

    if (!dates.Any())
    {
        return new StreakResult
        {
            TotalJournals = 0,
            CurrentStreak = 0,
            LongestStreak = 0
        };
    }

    int currentStreak = 0;
    var today = DateTime.Today;
    var dateSet = dates.ToHashSet();

    var check = today;
    while (dateSet.Contains(check))
    {
        currentStreak++;
        check = check.AddDays(-1);
    }

    int longestStreak = 1;
    int temp = 1;
    for (int i = 1; i < dates.Count; i++)
    {
        if ((dates[i] - dates[i - 1]).Days == 1)
            temp++;
        else
            temp = 1;

        longestStreak = Math.Max(longestStreak, temp);
    }

    return new StreakResult
    {
        TotalJournals = totalEntries,
        CurrentStreak = currentStreak,
        LongestStreak = longestStreak
    };
}
}