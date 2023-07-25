using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qurrah.Entities
{
    public class UserTypeDescription
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(UserType))]
        public UserTypeId FKUserTypeId { get; set; }

        [Required]
        [ForeignKey(nameof(Language))]
        public LanguageId FKLanguageId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        public UserType UserType { get; set; }
        public Language Language { get; set; }
    }
}