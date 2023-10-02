namespace Qi.Vision.WebApi.Features.IdentityCard.Front;

public interface IIdentityCardFrontAnalyzer
{
    public Task<IdentityCardFrontResponse> AnalyzeIdCardFrontAsync(
        string filePath,
        TrainningModelType trainningModelType = TrainningModelType.Template);
}
