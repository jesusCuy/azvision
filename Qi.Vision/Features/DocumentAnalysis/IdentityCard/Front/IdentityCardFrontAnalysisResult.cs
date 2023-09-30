using Azure.AI.FormRecognizer.DocumentAnalysis;

namespace Qi.Vision.WebApi.Features.DocumentAnalysis.IdentityCard.Front
{
    public class IdentityCardFrontAnalysisResult : AnalyzedDocumentMapper
    {
        public IdentityCardFrontAnalysisResult(AnalyzeResult result) : base(result)
        {
        }

        public AnalyzedDocumentField ElectorId => GetFieldValue("ELECTOR_KEY");
        public AnalyzedDocumentField Address => GetFieldValue("ADDRESS");
        public AnalyzedDocumentField BirthDate => GetFieldValue("BIRTH_DATE");
        public AnalyzedDocumentField Curp => GetFieldValue("CURP");
        public AnalyzedDocumentField Name => GetFieldValue("NAME");
    }
}
