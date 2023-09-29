using Azure.AI.FormRecognizer.DocumentAnalysis;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Qi.Vision.WebApi.Filters;

namespace Qi.Vision.WebApi.Controllers
{
    [Route("api/identity-cards")]
    [ApiController]
    public class IdentityCardController : ControllerBase
    {
        private const string UploadsFolderName = "Uploads";

        [HttpPost]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public async Task<IActionResult> AnalyzeFileAsync(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("Archivo no válido");
                }

                var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), UploadsFolderName);
                Directory.CreateDirectory(uploadsFolderPath);

                var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
                var filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);

                    Console.WriteLine($"File uploaded: {filePath}");
                    Console.WriteLine();
                }

                string key = "****";
                string endpoint = "***";

                Console.WriteLine("Azure Cognitive Services Computer Vision - .NET quickstart example");
                Console.WriteLine();

                AzureKeyCredential credential = new AzureKeyCredential(key);
                DocumentAnalysisClient client = new DocumentAnalysisClient(new Uri(endpoint), credential);

                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
                {

                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine("READ FILE FROM STREAM");
                    Console.WriteLine();

                    AnalyzeDocumentOperation operation = await client.AnalyzeDocumentAsync(
                        waitUntil: WaitUntil.Completed,
                        modelId: "INE-tm-template",
                        document: fileStream);

                    AnalyzeResult result = operation.Value;

                    Console.WriteLine($"Document was analyzed with model with ID: {result.ModelId}");

                    foreach (AnalyzedDocument document in result.Documents)
                    {
                        Console.WriteLine($"Document of type: {document.DocumentType}");

                        foreach (KeyValuePair<string, DocumentField> fieldKvp in document.Fields)
                        {
                            string fieldName = fieldKvp.Key;
                            DocumentField field = fieldKvp.Value;

                            Console.WriteLine($"Field '{fieldName}': ");

                            Console.WriteLine($"  Content: '{field.Content}'");
                            Console.WriteLine($"  Confidence: '{field.Confidence}'");
                        }
                    }
                }

                return Ok($"ID subido con éxito: {uniqueFileName}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al subir el archivo: {ex.Message}");
            }
        }
    }
}
