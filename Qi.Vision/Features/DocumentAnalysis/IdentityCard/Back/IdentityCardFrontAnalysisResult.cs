using Azure.AI.FormRecognizer.DocumentAnalysis;

namespace Qi.Vision.WebApi.Features.DocumentAnalysis.IdentityCard.Back
{
    public class IdentityCardBackAnalysisResult : AnalyzedDocumentMapper
    {
        public IdentityCardBackAnalysisResult(AnalyzeResult result) : base(result)
        {
        }

        public AnalyzedDocumentField ElectorId => GetFieldValue("ID_CODE");
    }
}
