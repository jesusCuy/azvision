using Azure.AI.FormRecognizer.DocumentAnalysis;
using Qi.Vision.WebApi.Features;

namespace Qi.Vision.WebApi.Services.DocumentIntelligence.TaxCertificate
{
    public class TaxCertificateAnalysisResult : AnalyzedDocumentMapper
    {
        public TaxCertificateAnalysisResult(AnalyzeResult result) : base(result) {}

        public FieldAnalysisResult? RFC => GetFieldValue("RFC");
        public FieldAnalysisResult? RFC_BarCode => GetFieldValue("RFC_BARCODE");
        public FieldAnalysisResult? RFC_QR => GetFieldValue("RFC_QR");
        public FieldAnalysisResult? CURP => GetFieldValue("CURP");
        public FieldAnalysisResult? Name => GetFieldValue("NAME");
        public FieldAnalysisResult? FirstLastName => GetFieldValue("FIRST_LASTNAME");
        public FieldAnalysisResult? SecondLastName => GetFieldValue("SECOND_LASTNAME");
        public FieldAnalysisResult? ZipCode
        {
            get
            {
                var zipCode = GetFieldValue("ZIP_CODE");

                if (zipCode != null)
                {
                    zipCode.Value = zipCode?.Value?.Substring(zipCode.Value.Length - 5);
                }

                return zipCode;
            }
        }

        public FieldAnalysisResult? HighwayType => GetFieldValue("HIGHWAY_TYPE");
        public FieldAnalysisResult? Street => GetFieldValue("STREET");
        public FieldAnalysisResult? ExteriorNumber => GetFieldValue("EXTERIOR_NUMBER");
        public FieldAnalysisResult? InteriorNumber => GetFieldValue("INTERIOR_NUMBER");
    }
}
