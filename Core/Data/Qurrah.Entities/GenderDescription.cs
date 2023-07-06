using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qurrah.Entities
{
    public class GenderDescription
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Gender))]
        public GenderId FKGenderId { get; set; }

        [Required]
        [ForeignKey(nameof(Language))]
        public LanguageId FKLanguageId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        public Gender Gender { get; set; }
        public Language Language { get; set; }
    }
}