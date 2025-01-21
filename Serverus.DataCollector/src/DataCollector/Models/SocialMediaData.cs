namespace Serverus.DataCollector.src.DataCollector.Models
{
    public record SocialMediaData
    {
        public string? Platform { get; init; }
        public string? Content { get; init; }
        public DateTime CollectedAt { get; init; }
    }
}
