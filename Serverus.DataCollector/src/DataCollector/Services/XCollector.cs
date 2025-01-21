using Serverus.DataCollector.src.DataCollector.Models;
using Serverus.DataCollector.src.DataCollector.Interfaces;

namespace Serverus.DataCollector.src.DataCollector.Services
{
    public class XCollector : ISocialMediaCollector
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<XCollector> _logger;

        public XCollector(HttpClient httpClient, ILogger<XCollector> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<SocialMediaData>> CollectDataAsync()
        {
            try
            {
                // Twitter API implementation
                return new List<SocialMediaData>
                {
                    new()
                    {
                        Platform = "Twitter",
                        Content = "Sample tweet",
                        CollectedAt = DateTime.UtcNow
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error collecting Twitter data");
                throw;
            }
        }
    }
}