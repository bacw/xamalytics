namespace Xamalytics.Common
{
    public class StoreResult
    {
        public StoreResult() { }
        public StoreResult(bool isSucceeded, string rawCopyFilePath, string ocrCopyFilePath)
        {
            IsSucceeded = isSucceeded;
            RawCopyFilePath = rawCopyFilePath;
            OcrCopyFilePath = ocrCopyFilePath;
        }

        public bool IsSucceeded { get; set; }
        public string? RawCopyFilePath { get; set; }
        public string? OcrCopyFilePath { get; set; }
    }
}
