using Microsoft.EntityFrameworkCore;

namespace Xamalytics.Data.Context
{
    public partial class XamalyticsContext : DbContext, IXamalyticsContext
    {
        public XamalyticsContext()
        {
        }

        public XamalyticsContext(DbContextOptions<XamalyticsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationError> ApplicationErrors { get; set; } = null!;
        public virtual DbSet<AuditDocument> AuditDocuments { get; set; } = null!;
        public virtual DbSet<AuditPatient> AuditPatients { get; set; } = null!;
        public virtual DbSet<AuditRefile> AuditRefiles { get; set; } = null!;
        public virtual DbSet<AuditUser> AuditUsers { get; set; } = null!;
        public virtual DbSet<ActivityType> ActivityTypes { get; set; } = null!;
        public virtual DbSet<Batch> Batches { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<BatchSource> BatchSources { get; set; } = null!;
        public virtual DbSet<ClaimStatuses> ClaimStatuses { get; set; } = null!;

        public virtual DbSet<BatchSourceConfig> BatchSourceConfigs { get; set; } = null!;
        public virtual DbSet<BatchStatus> BatchStatuses { get; set; } = null!;
        public virtual DbSet<Document> Documents { get; set; } = null!;
        public virtual DbSet<Ingestion> Ingestions { get; set; } = null!;
        public virtual DbSet<IngestionStatus> IngestionStatuses { get; set; } = null!;
        public virtual DbSet<IngestionType> IngestionTypes { get; set; } = null!;
        public virtual DbSet<Patient> Patients { get; set; } = null!;
        public virtual DbSet<Refile> Refiles { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<RequestStatus> RequestStatuses { get; set; } = null!;
        public virtual DbSet<RequestType> RequestTypes { get; set; } = null!;
        public virtual DbSet<StagingIngestion> StagingIngestions { get; set; } = null!;
        public virtual DbSet<StagingMetadatum> StagingMetadata { get; set; } = null!;
        public virtual DbSet<StagingPatient> StagingPatients { get; set; } = null!;
        public virtual DbSet<StagingRefile> StagingRefiles { get; set; } = null!;
        public virtual DbSet<StagingRequest> StagingRequests { get; set; } = null!;
        public virtual DbSet<StagingRequestStatus> StagingRequestStatuses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationError>(entity =>
            {
                entity.ToTable("ApplicationError", "adt");

                entity.Property(e => e.Environment).HasMaxLength(1000);

                entity.Property(e => e.LogLevel).HasMaxLength(50);

                entity.Property(e => e.Logger).HasMaxLength(500);

                entity.Property(e => e.MachineName).HasMaxLength(100);

                entity.Property(e => e.Thread).HasMaxLength(1000);

                entity.Property(e => e.Timestamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<AuditDocument>(entity =>
            {
                entity.HasKey(e => new { e.DocumentId, e.DmlType, e.DmlTimestamp })
                    .HasName("PK__AuditDoc__BD52433BC7741B07");

                entity.ToTable("AuditDocument", "adt");

                entity.Property(e => e.DmlType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DmlTimestamp).HasColumnType("datetime");

                entity.Property(e => e.DmlCreatedBy)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.NewRowData).HasMaxLength(1000);

                entity.Property(e => e.OldRowData).HasMaxLength(1000);

                entity.Property(e => e.TransactionTimestamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditPatient>(entity =>
            {
                entity.HasKey(e => new { e.PatientId, e.DmlType, e.DmlTimestamp })
                    .HasName("PK__AuditPat__30E26F520A68D9B7");

                entity.ToTable("AuditPatient", "adt");

                entity.Property(e => e.DmlType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DmlTimestamp).HasColumnType("datetime");

                entity.Property(e => e.DmlCreatedBy)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.NewRowData).HasMaxLength(1000);

                entity.Property(e => e.OldRowData).HasMaxLength(1000);

                entity.Property(e => e.TransactionTimestamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditRefile>(entity =>
            {
                entity.HasKey(e => new { e.RefileId, e.DmlType, e.DmlTimestamp })
                    .HasName("PK__AuditRef__084D33B6CA6B07EE");

                entity.ToTable("AuditRefile", "adt");

                entity.Property(e => e.DmlType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DmlTimestamp).HasColumnType("datetime");

                entity.Property(e => e.DmlCreatedBy)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.NewRowData).HasMaxLength(1000);

                entity.Property(e => e.OldRowData).HasMaxLength(1000);

                entity.Property(e => e.TransactionTimestamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<AuditUser>(entity =>
            {                

                entity.ToTable("AuditUser", "adt");

                entity.Property(e => e.ActivityDate).HasColumnType("datetime");

                entity.Property(e => e.ActivityDetails).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(200);

                entity.HasOne(d => d.ActivityType)
                    .WithMany(p => p.AuditUsers)
                    .HasForeignKey(d => d.ActivityTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditUser_ActivityType");

            });

            modelBuilder.Entity<ActivityType>(entity =>
            {
                entity.ToTable("ActivityType", "lkp");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(100);
            });


            modelBuilder.Entity<Batch>(entity =>
            {
                entity.ToTable("Batch", "trn");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LastModifiedBy).HasMaxLength(50);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ProcessEndDate).HasColumnType("datetime");

                entity.Property(e => e.ProcessStartDate).HasColumnType("datetime");

                entity.Property(e => e.TotalNumberOfFilesInBatch).HasMaxLength(50);

                entity.Property(e => e.CurrentNumberOfFilesProcessedInBatch).HasMaxLength(50);

                entity.Property(e => e.Output).HasMaxLength(50);

                entity.HasOne(d => d.BatchStatus)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.BatchStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Batch_BatchStatus");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "trn");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.ClaimStatus)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ClaimStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_ClaimStatus");
            });

            modelBuilder.Entity<BatchSource>(entity =>
            {
                entity.ToTable("BatchSource", "lkp");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<BatchSourceConfig>(entity =>
            {
                entity.ToTable("BatchSourceConfig", "lkp");

                entity.HasIndex(e => e.BatchSourceId, "IX_BatchSourceConfig_BatchSourceId");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.AllowedBatchFileExtensions).HasMaxLength(50);

                entity.Property(e => e.AllowedDocumentFileExtensions).HasMaxLength(50);

                entity.Property(e => e.AllowedManifestFileExtensions).HasMaxLength(50);

                entity.Property(e => e.CompletedPath).HasMaxLength(1000);

                entity.Property(e => e.DocumentStoreRootPath).HasMaxLength(1000);

                entity.Property(e => e.DuplicatePath).HasMaxLength(1000);

                entity.Property(e => e.FailedPath).HasMaxLength(1000);

                entity.Property(e => e.FtpUri).HasMaxLength(1000);

                entity.Property(e => e.IsImplicitSsl).HasColumnName("IsImplicitSSL");

                entity.Property(e => e.Matcher).HasMaxLength(1000);

                entity.Property(e => e.ManifestSchemaPaths).HasMaxLength(1000);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.RelativePath).HasMaxLength(1000);

                entity.Property(e => e.StagingPath).HasMaxLength(1000);

                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.HasOne(d => d.BatchSource)
                    .WithMany(p => p.BatchSourceConfigs)
                    .HasForeignKey(d => d.BatchSourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BatchSourceConfig_BatchSource");
            });

            modelBuilder.Entity<BatchStatus>(entity =>
            {
                entity.ToTable("BatchStatus", "lkp");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<BatchStatus>(entity =>
            {
                entity.ToTable("BatchStatus", "lkp");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<ClaimStatuses>(entity =>
            {
                entity.ToTable("ClaimStatus", "lkp");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("Document", "trn", r => r.HasTrigger("TriggerDocumentInsert"));

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DocumentDate).HasColumnType("datetime");

                entity.Property(e => e.DocumentType).HasMaxLength(100);

                entity.Property(e => e.ExternalDocumentId).HasMaxLength(200);

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.LastModifiedBy).HasMaxLength(50);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OcrcopyFilePath)
                    .HasMaxLength(2000)
                    .HasColumnName("OCRCopyFilePath");

                entity.Property(e => e.RawCopyFilePath).HasMaxLength(2000);

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.HasOne(d => d.LastIngestion)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.LastIngestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Document_Ingestion");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Document_Patient");
            });

            modelBuilder.Entity<Ingestion>(entity =>
            {
                entity.ToTable("Ingestion", "trn");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(50);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.IngestionStatus)
                    .WithMany(p => p.Ingestions)
                    .HasForeignKey(d => d.IngestionStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ingestion_IngestionStatus");

                entity.HasOne(d => d.IngestionType)
                    .WithMany(p => p.Ingestions)
                    .HasForeignKey(d => d.IngestionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ingestion_IngestionType");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.Ingestions)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ingestion_Request");
            });

            modelBuilder.Entity<IngestionStatus>(entity =>
            {
                entity.ToTable("IngestionStatus", "lkp");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<IngestionType>(entity =>
            {
                entity.ToTable("IngestionType", "lkp");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patient", "trn", r => r.HasTrigger("TriggerPatientInsert"));

                entity.Property(e => e.Address).HasMaxLength(4000);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Dob).HasColumnType("date");

                entity.Property(e => e.EmailAddress).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.HomePhoneNumber).HasMaxLength(15);

                entity.Property(e => e.LastModifiedBy).HasMaxLength(50);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MobilePhoneNumber).HasMaxLength(15);

                entity.Property(e => e.Mrn)
                    .HasMaxLength(50)
                    .HasColumnName("MRN");

                entity.Property(e => e.Nhsnumber)
                    .HasMaxLength(50)
                    .HasColumnName("NHSNumber");

                entity.Property(e => e.Title).HasMaxLength(20);

                entity.Property(e => e.WorkPhoneNumber).HasMaxLength(15);

                entity.HasOne(d => d.LastIngestion)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.LastIngestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patient_Ingestion");
            });

            modelBuilder.Entity<Refile>(entity =>
            {
                entity.ToTable("Refile", "trn", r => r.HasTrigger("TriggerPatientInsert"));

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(50);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.SourceExternalDocumentId).HasMaxLength(200);

                entity.Property(e => e.SourcePatientMrn)
                    .HasMaxLength(50)
                    .HasColumnName("SourcePatientMRN");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.Refiles)
                    .HasForeignKey(d => d.DocumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Refile_Document");

                entity.HasOne(d => d.LastIngestion)
                    .WithMany(p => p.Refiles)
                    .HasForeignKey(d => d.LastIngestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Refile_Ingestion");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("Request", "trn");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(50);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ProcessEndDate).HasColumnType("datetime");

                entity.Property(e => e.ProcessStartDate).HasColumnType("datetime");

                entity.HasOne(d => d.RequestStatus)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.RequestStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Submission_SubmissionStatus");

                entity.HasOne(d => d.RequestType)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.RequestTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Submission_SubmissionType");               
            });

            modelBuilder.Entity<RequestStatus>(entity =>
            {
                entity.ToTable("RequestStatus", "lkp");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<RequestType>(entity =>
            {
                entity.ToTable("RequestType", "lkp");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<StagingIngestion>(entity =>
            {
                entity.ToTable("StagingIngestion", "stg");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.StagingIngestions)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StagingIngestion_Request");
            });

            modelBuilder.Entity<StagingMetadatum>(entity =>
            {
                entity.ToTable("StagingMetadata", "stg");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DocumentDate).HasColumnType("datetime");

                entity.Property(e => e.DocumentType).HasMaxLength(100);

                entity.Property(e => e.ExternalDocumentId).HasMaxLength(200);

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.Mrn)
                    .HasMaxLength(50)
                    .HasColumnName("MRN");

                entity.Property(e => e.RawCopyFilePath).HasMaxLength(2000);

                entity.Property(e => e.Title).HasMaxLength(500);

                entity.HasOne(d => d.StagingIngestion)
                    .WithMany(p => p.StagingMetadata)
                    .HasForeignKey(d => d.StagingIngestionId)
                    .HasConstraintName("FK_StagingMetadata_StagingIngestion");
            });

            modelBuilder.Entity<StagingPatient>(entity =>
            {
                entity.ToTable("StagingPatient", "stg");

                entity.Property(e => e.Address).HasMaxLength(4000);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Dob).HasColumnType("date");

                entity.Property(e => e.EmailAddress).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.HomePhoneNumber).HasMaxLength(15);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MobilePhoneNumber).HasMaxLength(15);

                entity.Property(e => e.Mrn)
                    .HasMaxLength(50)
                    .HasColumnName("MRN");

                entity.Property(e => e.Nhsnumber)
                    .HasMaxLength(50)
                    .HasColumnName("NHSNumber");

                entity.Property(e => e.Title).HasMaxLength(20);

                entity.Property(e => e.WorkPhoneNumber).HasMaxLength(15);

                entity.HasOne(d => d.StagingIngestion)
                    .WithMany(p => p.StagingPatients)
                    .HasForeignKey(d => d.StagingIngestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StagingPatient_StagingIngestion");
            });

            modelBuilder.Entity<StagingRefile>(entity =>
            {
                entity.ToTable("StagingRefile", "stg");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ExternalDocumentId).HasMaxLength(200);

                entity.Property(e => e.SourceExternalDocumentId).HasMaxLength(200);

                entity.Property(e => e.SourceMrn)
                    .HasMaxLength(50)
                    .HasColumnName("SourceMRN");

                entity.HasOne(d => d.StagingIngestion)
                    .WithMany(p => p.StagingRefiles)
                    .HasForeignKey(d => d.StagingIngestionId)
                    .HasConstraintName("FK_StagingRefile_StagingIngestion");
            });

            modelBuilder.Entity<StagingRequest>(entity =>
            {
                entity.ToTable("StagingRequest", "stg");

                entity.Property(e => e.BatchFileName).HasMaxLength(4000);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.StagingRequestStatus)
                    .WithMany(p => p.StagingRequests)
                    .HasForeignKey(d => d.StagingRequestStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StagingRequest_StagingRequestStatus");
            });

            modelBuilder.Entity<StagingRequestStatus>(entity =>
            {
                entity.ToTable("StagingRequestStatus", "lkp");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
