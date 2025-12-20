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
    public async Task<List<Journal>> GetAllJournalsAsync()
    {
        return await dbConfig.Journals
            .Include(j => j.Mood)
            .Include(j => j.Tags)
            .ToListAsync();
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
}