using Mitra_Journal.Entities;

namespace Mitra_Journal.Services.Interface;

public interface ITagsService
{
    List<Tags> GetTags();    
    Tags AddTag(string tag);
    Dictionary<string, int> GetTagsAnalytics();
}