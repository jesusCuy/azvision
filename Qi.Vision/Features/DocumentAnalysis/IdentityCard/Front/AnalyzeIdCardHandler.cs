using Azure.AI.FormRecognizer.DocumentAnalysis;
using Azure;
using MediatR;

namespace Qi.Vision.WebApi.Features.DocumentAnalysis.IdentityCard.Front
{
    public class AnalyzeIdCardFrontHandler : IRequestHandler<AnalyzeIdCardFrontCommand, IdentityCardAnalysisResult>
    {
        private const string UPLOADS_FOLDER_NAME = "Uploads/identity-cards/front";
        private readonly string _apiKey = "***";
        private readonly string _endpoint = "***";

        public async Task<IdentityCardAnalysisResult> Handle(AnalyzeIdCardFrontCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var filePath = SaveFile(request.File);

                var analysis = await AnalyzeDocumentAsync(
                    filePath: filePath,
                    modelType: request.ModelType
                );

                return new IdentityCardAnalysisResult(analysis.Value);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"An error occurred during file analysis: {ex.Message}");
            }
        }

        private async Task<AnalyzeDocumentOperation> AnalyzeDocumentAsync(
            string filePath, 
            TrainningModelType modelType)
        {

            var client = CreateDocumentAnalysisClient();

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                var modelId = modelType switch
                {
                    TrainningModelType.Template => "tm-template-identity-card-front-29092023.01",
                    TrainningModelType.Neural => throw new NotImplementedException($"Not implemented yet: {nameof(TrainningModelType.Neural)}"),
                    TrainningModelType.Composed => throw new NotImplementedException($"Not implemented yet: {nameof(TrainningModelType.Composed)}"),
                    _ => "tm-template-identity-card-front-29092023.01"
                };

                return await client.AnalyzeDocumentAsync(
                    waitUntil: WaitUntil.Completed,
                    modelId: modelId,
                    document: fileStream
                );
            }
        }

        private DocumentAnalysisClient CreateDocumentAnalysisClient()
        {
            var credential = new AzureKeyCredential(_apiKey);
            return new DocumentAnalysisClient(new Uri(_endpoint), credential);
        }

        private string SaveFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is null");
            }

            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), UPLOADS_FOLDER_NAME);
            Directory.CreateDirectory(uploadsFolderPath);

            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);

                Console.WriteLine($"File uploaded: {filePath}");
                Console.WriteLine();
            }

            return filePath;
        }

    }
}
