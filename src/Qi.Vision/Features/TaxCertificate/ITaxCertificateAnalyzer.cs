namespace Qi.Vision.WebApi.Features.TaxCertificate
{
    public interface ITaxCertificateAnalyzer
    {
        public Task<TaxCertificateResponse> AnalyzeTaskCertificateAsync(
            string filePath,
            TrainningModelType trainningModelType = TrainningModelType.Template);
    }

}
