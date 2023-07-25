using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qurrah.Entities
{
    public class CenterType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public CenterTypeId Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public List<CenterTypeDescription> CenterTypeDescriptions { get; set; }
    }
}