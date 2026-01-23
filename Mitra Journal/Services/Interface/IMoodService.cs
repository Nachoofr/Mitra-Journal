using Mitra_Journal.Entities;

namespace Mitra_Journal.Services;

public interface IMoodService
{
    List<Mood> GetAllMoods();
    Dictionary<string,int> GetMoodAnalytics();
}