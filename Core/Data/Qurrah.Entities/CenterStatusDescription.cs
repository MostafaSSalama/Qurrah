using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qurrah.Entities
{
    public class CenterStatusDescription
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(CenterType))]
        public CenterStatusId FKCenterStatusId { get; set; }

        [Required]
        [ForeignKey(nameof(Language))]
        public LanguageId FKLanguageId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        public CenterStatus CenterStatus { get; set; }
        public Language Language { get; set; }
    }
}