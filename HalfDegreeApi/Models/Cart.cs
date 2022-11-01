using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HalfDegreeApi.Models
{
    public class Cart
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderdId { get; set; }
        [Required]
        public string? AddedBy { get; set; }
        



    }
}
