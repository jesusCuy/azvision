namespace Qi.Vision.WebApi.Features.DocumentAnalysis
{
    public class DocAnalysisOptions
    {
        public const string DocAnalysis = "DocAnalysis";

        public string API_KEY { get; set; } = string.Empty;
        public string ENDPOINT { get; set; } = string.Empty;
        public string ID_CARD_FRONT_TEMPLATE { get; set; } = string.Empty;
        public string ID_CARD_FRONT_NEURAL { get; set; } = string.Empty;
        public string ID_CARD_BACK_TEMPLATE { get; set; } = string.Empty;
    }
}
