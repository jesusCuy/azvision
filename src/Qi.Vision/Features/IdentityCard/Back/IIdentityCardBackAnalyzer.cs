namespace Qi.Vision.WebApi.Features.IdentityCard.Back
{
    public interface IIdentityCardBackAnalyzer
    {
        public Task<IdentityCardBackResponse> AnalyzeIdCardBackAsync(
            string filePath,
            TrainningModelType trainningModelType = TrainningModelType.Template);
    }
}
