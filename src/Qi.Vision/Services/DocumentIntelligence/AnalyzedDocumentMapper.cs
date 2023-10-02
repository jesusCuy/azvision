using Azure.AI.FormRecognizer.DocumentAnalysis;
using Qi.Vision.WebApi.Features;
using System.Text.RegularExpressions;

namespace Qi.Vision.WebApi.Services.DocumentIntelligence
{
    public class AnalyzedDocumentMapper
    {
        private readonly AnalyzedDocument doc;

        public AnalyzedDocumentMapper(AnalyzeResult result)
        {
            Console.WriteLine($"Document was analyzed with model with ID: {result.ModelId}");

            var document = result.Documents.FirstOrDefault();

            if (document is null)
            {
                throw new Exception("Error during document analysis. Result is null");
            }

            doc = document ?? throw new ArgumentNullException(nameof(document));
        }

        public FieldAnalysisResult GetFieldValue(string key)
        {
            var field = doc.Fields.SingleOrDefault(x => x.Key == key);

            return field.Value != null
                ? new FieldAnalysisResult
                {
                    Value = field.Value.Content is null
                                ? string.Empty
                                : Regex.Replace(field.Value.Content, @"\t|\n|\r", " "),
                    Confidence = field.Value.Confidence
                }
                : new FieldAnalysisResult();
        }
    }
}
