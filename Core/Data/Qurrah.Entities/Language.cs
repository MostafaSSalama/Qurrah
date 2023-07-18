using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qurrah.Entities
{
    public class Language
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public LanguageId Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(20)]
        public string LanguageCulture { get; set; }

        [Required]
        public int DisplayOrder { get; set; }

        [Required]
        public bool RTL { get; set; }

        [Required]
        public bool Published { get; set; }

        public List<LanguageDescription> LanguageDescriptions { get; set; }
        public List<LanguageDescription> InLanguageDescriptions { get; set; }
        public List<GenderDescription> GenderDescriptions { get; set; }
        public List<LocalizedProperty> LocalizedProperties { get; set; }
        public List<CenterLicenseStatusDescription> CenterLicenseStatusDescriptions { get; set; }
    }
}