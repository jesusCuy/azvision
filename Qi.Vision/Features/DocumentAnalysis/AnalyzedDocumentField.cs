namespace Qi.Vision.WebApi.Features.DocumentAnalysis
{
    public class AnalyzedDocumentField
    {
        public string Content { get; set; } = string.Empty;
        public float? Confidence { get; set; }
    }
}
