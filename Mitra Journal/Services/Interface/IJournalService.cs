using Mitra_Journal.Entities;

namespace Mitra_Journal.Services.Interface;

public interface IJournalService
{
    Task<(int,List<Journal>)> GetAllJournalsAsync(int pageNo=1,int pageSize = 5);

    Task<StreakResult> GetStreaksAsync();
    Task<Journal> GetJournalByIdAsync(Guid id);
    
    Task AddJournalAsync(Journal journal);
    
    Task UpdateJournalAsync(Journal journal);
    
    Task DeleteJournalAsync(Guid id);
}