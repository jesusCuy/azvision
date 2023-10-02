namespace Qi.Vision.WebApi.Features.TaxCertificate
{
    public class TaxCertificateResponse
    {
        public FieldAnalysisResult? RFC { get; set; }
        public FieldAnalysisResult? CURP { get; set; }
        public FieldAnalysisResult? Name { get; set; }
        public FieldAnalysisResult? FirstLastName { get; set; }
        public FieldAnalysisResult? SecondLastName { get; set; }
        public FieldAnalysisResult? ZipCode { get; set; }
    }
}
