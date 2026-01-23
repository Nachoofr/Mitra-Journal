using Mitra_Journal.Entities;

public interface IJournalPdfService
{
    Task<byte[]> ExportJournalToPdfAsync(DateTime fromDate, DateTime toDate);
}