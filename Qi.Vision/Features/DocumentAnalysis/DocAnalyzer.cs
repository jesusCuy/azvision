using Azure.AI.FormRecognizer.DocumentAnalysis;
using Azure;
using Microsoft.Extensions.Options;

namespace Qi.Vision.WebApi.Features.DocumentAnalysis
{
    public class DocAnalyzer
    {
        public DocAnalysisOptions _options { get; set; }

        public DocAnalyzer(IOptions<DocAnalysisOptions> options)
        {
            _options = options.Value;
        }

        public async Task<AnalyzeDocumentOperation> AnalyzeDocumentAsync(
            string filePath,
            string modelId)
        {
            var credential = new AzureKeyCredential(_options.API_KEY);
            var client = new DocumentAnalysisClient(new Uri(_options.ENDPOINT), credential);

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                return await client.AnalyzeDocumentAsync(
                    waitUntil: WaitUntil.Completed,
                    modelId: modelId,
                    document: fileStream
                );
            }
        }
    }
}
