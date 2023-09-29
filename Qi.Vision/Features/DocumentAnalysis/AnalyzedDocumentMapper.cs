using Azure.AI.FormRecognizer.DocumentAnalysis;
using System.Text.RegularExpressions;

namespace Qi.Vision.WebApi.Features.DocumentAnalysis
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

        public AnalyzedDocumentField GetFieldValue(string key)
        {
            var field = doc.Fields.SingleOrDefault(x => x.Key == key);

            return field.Value != null
                ? new AnalyzedDocumentField 
                  { 
                    Content = Regex.Replace(field.Value.Content, @"\t|\n|\r", " "), 
                    Confidence = field.Value.Confidence 
                  }
                : new AnalyzedDocumentField();
        }
    }
}
