using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Microsoft.EntityFrameworkCore;
using Mitra_Journal.Data;
using System.IO;
using Syncfusion.Drawing; // For RectangleF
using System.Text.RegularExpressions;
using System.Net;

namespace Mitra_Journal.Services;

public class JournalPdfService : IJournalPdfService
{
    private readonly LocalDbContext _dbContext;

    public JournalPdfService(LocalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Helper to strip HTML tags and decode HTML entities
    private string StripHtml(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        // Remove all HTML tags
        string result = Regex.Replace(input, "<.*?>", string.Empty);

        // Decode HTML entities like &amp; → &
        result = WebUtility.HtmlDecode(result);

        return result;
    }

    public async Task<byte[]> ExportJournalToPdfAsync(DateTime fromDate, DateTime toDate)
    {
        // Fetch journal entries from EF
        var entries = await _dbContext.Journals
            .Where(j => j.CreatedAt >= fromDate && j.CreatedAt <= toDate)
            .OrderBy(j => j.CreatedAt)
            .ToListAsync();

        // Create PDF document
        using var document = new PdfDocument();
        document.PageSettings.Orientation = PdfPageOrientation.Portrait;

        // Add the first page
        PdfPage page = document.Pages.Add();
        PdfGraphics graphics = page.Graphics;

        float yPosition = 20; // Starting Y position
        PdfFont fontTitle = new PdfStandardFont(PdfFontFamily.Helvetica, 16, PdfFontStyle.Bold);
        PdfFont fontDate = new PdfStandardFont(PdfFontFamily.Helvetica, 10, PdfFontStyle.Italic);
        PdfFont fontContent = new PdfStandardFont(PdfFontFamily.Helvetica, 12);

        foreach (var entry in entries)
        {
            // Check if we need a new page
            if (yPosition > page.GetClientSize().Height - 100)
            {
                page = document.Pages.Add();
                graphics = page.Graphics;
                yPosition = 20;
            }

            // Draw Date
            graphics.DrawString(
                entry.CreatedAt.ToString("yyyy-MM-dd"),
                fontDate,
                PdfBrushes.Gray,
                new RectangleF(10, yPosition, page.GetClientSize().Width - 20, 20)
            );
            yPosition += 25;

            // Draw Title
            graphics.DrawString(
                StripHtml(entry.Title),
                fontTitle,
                PdfBrushes.Black,
                new RectangleF(10, yPosition, page.GetClientSize().Width - 20, 30)
            );
            yPosition += 35;

            // Draw Content (wrap text)
            string plainText = StripHtml(entry.Description);
            PdfTextElement textElement = new PdfTextElement(plainText, fontContent, PdfBrushes.Black);

            // Use PdfLayoutFormat to wrap text and paginate
            PdfLayoutFormat layoutFormat = new PdfLayoutFormat
            {
                Layout = PdfLayoutType.Paginate
            };

            var bounds = new RectangleF(10, yPosition, page.GetClientSize().Width - 20, page.GetClientSize().Height - yPosition);
            PdfLayoutResult result = textElement.Draw(page, bounds, layoutFormat);

            yPosition = result.Bounds.Bottom + 20;
        }

        // Save PDF to memory
        using var stream = new MemoryStream();
        document.Save(stream);
        document.Close(true);

        return stream.ToArray(); // Return PDF as byte array
    }
}