using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gawektree_back.Entities
{
    public class Record
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid guid { get; set; }
        [Required]
        public String? name { get; set;  }
        [Required]
        public Guid parentGuid { get; set; }
    }
}
