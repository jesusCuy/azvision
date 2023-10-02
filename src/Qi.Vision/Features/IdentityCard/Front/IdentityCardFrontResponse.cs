namespace Qi.Vision.WebApi.Features.IdentityCard.Front
{
    public class IdentityCardFrontResponse
    {
        public FieldAnalysisResult? FullName { get; set; }
        public FieldAnalysisResult? Address { get; set; }
        public FieldAnalysisResult? CURP { get; set; }
        public FieldAnalysisResult? ElectorKey { get; set; }
        public FieldAnalysisResult? BirthDate { get; set; }
    }
}
