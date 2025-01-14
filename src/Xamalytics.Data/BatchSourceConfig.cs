using System;
using System.Collections.Generic;

namespace Xamalytics.Data
{
    public partial class BatchSourceConfig
    {
        public long Id { get; set; }
        public long BatchSourceId { get; set; }
        public string? FtpUri { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? RelativePath { get; set; }
        public bool? IsSecure { get; set; }
        public string? Matcher { get; set; }
        public bool? IsImplicitSsl { get; set; }
        public string CompletedPath { get; set; } = null!;
        public string FailedPath { get; set; } = null!;
        public string DuplicatePath { get; set; } = null!;
        public string StagingPath { get; set; } = null!;
        public string? DocumentStoreRootPath { get; set; }
        public string? AllowedBatchFileExtensions { get; set; }
        public string? AllowedManifestFileExtensions { get; set; }
        public string? AllowedDocumentFileExtensions { get; set; }
        public string? ManifestSchemaPaths { get; set; }
        public bool? IsActive { get; set; }

        public virtual BatchSource BatchSource { get; set; } = null!;
    }
}
