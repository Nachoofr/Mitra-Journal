using Mitra_Journal.Entities;

namespace Mitra_Journal.Services.Interface;

public interface IJournalService
{
    Task<List<Journal>> GetAllJournalsAsync();
    
    Task<Journal> GetJournalByIdAsync(Guid id);
    
    Task AddJournalAsync(Journal journal);
    
    Task UpdateJournalAsync(Journal journal);
    
    Task DeleteJournalAsync(Guid id);
}