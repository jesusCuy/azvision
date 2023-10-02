using Azure.AI.FormRecognizer.DocumentAnalysis;
using Qi.Vision.WebApi.Features;

namespace Qi.Vision.WebApi.Services.DocumentIntelligence.IdentityCard.Front
{
    public class IdentityCardFrontAnalysisResult : AnalyzedDocumentMapper
    {
        public IdentityCardFrontAnalysisResult(AnalyzeResult result) : base(result)
        {
        }

        public FieldAnalysisResult ElectorId => GetFieldValue("ELECTOR_KEY");
        public FieldAnalysisResult Address => GetFieldValue("ADDRESS");
        public FieldAnalysisResult BirthDate => GetFieldValue("BIRTH_DATE");
        public FieldAnalysisResult Curp => GetFieldValue("CURP");
        public FieldAnalysisResult Name => GetFieldValue("NAME");
    }
}
