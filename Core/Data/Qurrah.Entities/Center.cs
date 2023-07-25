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
        public string Name { get; set; }

        [StringLength(24, MinimumLength = 24)]
        public string IBAN { get; set; }

        [ForeignKey(nameof(CenterType))]
        public CenterTypeId? FKCenterTypeId { get; set; }
        public CenterType CenterType { get; set; }

        [ForeignKey(nameof(File))]
        public Guid? FKIBANFileId { get; set; }
        public FileDetails File { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        [ForeignKey(nameof(CenterStatus))]
        public CenterStatusId FKCenterStatusId { get; set; }
        public CenterStatus CenterStatus { get; set; }

        [Required]
        public DateTime StatusDate { get; set; }

        public string RejectionReason { get; set; }

        [ForeignKey(nameof(StatusUpdatedByUser))]
        public string FKStatusUpdatedByUserId { get; set; }
        public ApplicationUser StatusUpdatedByUser { get; set; }
    }
}