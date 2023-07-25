using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Qurrah.Entities
{
    public class CenterStatus
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public CenterStatusId Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public List<CenterStatusDescription> CenterStatusDescriptions { get; set; }
    }
}