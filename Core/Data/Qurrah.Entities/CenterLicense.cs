﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qurrah.Entities
{
    public class CenterLicense
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string LicenseNumber { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        [ForeignKey(nameof(Center))]
        public int FKCenterId { get; set; }
        public Center Center { get; set; }

        [Required]
        [ForeignKey(nameof(FileDetails))]
        public Guid FKFileId { get; set; }
        public FileDetails FileDetails { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        [ForeignKey(nameof(Status))]
        public CenterLicenseStatusId FKStatusId { get; set; }
        public CenterLicenseStatus Status { get; set; }

        public string RejectionReason { get; set; }

        [Required]
        public DateTime StatusDate { get; set; }
        
        [Required]
        [ForeignKey(nameof(CreatedByUser))]
        public string FKCreatedByUserId { get; set; }
        public virtual ApplicationUser CreatedByUser { get; set; }

        [ForeignKey(nameof(StatusUpdatedByUser))]
        public string FKStatusUpdatedByUserId { get; set; }
        public ApplicationUser StatusUpdatedByUser { get; set; }
    }
}