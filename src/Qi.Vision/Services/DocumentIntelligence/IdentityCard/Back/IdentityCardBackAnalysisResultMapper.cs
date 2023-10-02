using Azure.AI.FormRecognizer.DocumentAnalysis;
using Qi.Vision.WebApi.Features;

namespace Qi.Vision.WebApi.Services.DocumentIntelligence.IdentityCard.Back
{
    public class IdentityCardBackAnalysisResultMapper : AnalyzedDocumentMapper
    {
        public IdentityCardBackAnalysisResultMapper(AnalyzeResult result) : base(result)
        {
        }

        public FieldAnalysisResult ElectorId => GetFieldValue("ID_CODE");
    }
}
