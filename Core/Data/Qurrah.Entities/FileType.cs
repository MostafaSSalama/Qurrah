using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Qurrah.Entities
{
    public class FileType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public FileTypeId Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public List<FileDetails> FileDetails { get; set; }
    }
}