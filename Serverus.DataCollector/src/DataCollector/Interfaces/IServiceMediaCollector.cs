using Serverus.DataCollector.src.DataCollector.Models;

namespace Serverus.DataCollector.src.DataCollector.Interfaces
{
    public interface ISocialMediaCollector
    {
        Task<IEnumerable<SocialMediaData>> CollectDataAsync();
    }
}
