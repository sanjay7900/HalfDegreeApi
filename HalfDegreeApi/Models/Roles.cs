using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HalfDegreeApi.Models
{
    public class Roles
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public  int Id { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
