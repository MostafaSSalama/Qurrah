using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qurrah.Entities
{
    public class Center
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(AllowEmptyStrings = false)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(500)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(24, MinimumLength = 24)]
        public string IBAN { get; set; }

        [Required]
        [ForeignKey(nameof(CenterType))]
        public CenterTypeId FKCenterTypeId { get; set; }
        public virtual CenterType CenterType { get; set; }

        [Required]
        [ForeignKey(nameof(File))]
        public Guid FKIBANFileId { get; set; }
        public virtual FileDetails File { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        [ForeignKey(nameof(CreatedByUser))]
        public string FKCreatedByUserId { get; set; }
        public virtual ApplicationUser CreatedByUser { get; set; }

        [Required]
        [ForeignKey(nameof(CenterStatus))]
        public CenterStatusId FKCenterStatusId { get; set; }
        public CenterStatus CenterStatus { get; set; }

        [Required]
        public DateTime StatusDate { get; set; }

        public string RejectionReason { get; set; }

        [ForeignKey(nameof(StatusUpdatedByUser))]
        public string FKStatusUpdatedByUserId { get; set; }
        public virtual ApplicationUser StatusUpdatedByUser { get; set; }

        public virtual List<CenterUser> CenterUsers { get; set; }
        public virtual List<CenterLicense> CenterLicenses { get; set; }
    }
}