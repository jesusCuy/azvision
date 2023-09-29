using System.Security.Cryptography;

namespace Qi.Vision.WebApi.Features.DocumentAnalysis
{
    public class FileHash
    {
        public static string Calculate(string filePath)
        {
            using (var sha256 = SHA256.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    byte[] hashBytes = sha256.ComputeHash(stream);
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }
            }
        }
    }
}
