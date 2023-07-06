using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qurrah.Entities
{
    public class Gender
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public GenderId Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public List<ParentUser> ParentUsers { get; set; }
        public List<CenterOwnerUser> CenterOwnerUsers { get; set; }
        public List<GenderDescription> GenderDescriptions { get; set; }
    }
}