namespace AIAgentTextSummarization.Options
{
    public class OpenAIConfiguration
    {
        public const string SectionName = "OpenAIConfiguration";
        public string Endpoint { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string ModelId { get; set; } = string.Empty;
        public int MaxTokens { get; set; }
        public double Temperature { get; set; }
        public double TopP { get; set; }
    }
}
