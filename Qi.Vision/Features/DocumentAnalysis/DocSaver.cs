namespace Qi.Vision.WebApi.Features.DocumentAnalysis
{
    public class DocSaver
    {
        public string SaveFileAs(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is null");
            }

            var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), $"Uploads/{folder}");
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
