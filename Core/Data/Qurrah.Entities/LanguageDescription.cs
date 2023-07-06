using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qurrah.Entities
{
    public class LanguageDescription
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Language))]
        public LanguageId FKLanguageId { get; set; }

        [Required]
        [ForeignKey(nameof(InLanguage))]
        public LanguageId FKInLanguageId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        public Language Language { get; set; }
        public Language InLanguage { get; set; }
    }
}
