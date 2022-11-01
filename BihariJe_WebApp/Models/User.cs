using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BihariJe_WebApp.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(50)]
        public string? Name { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime? CreatedDate { get; set; }
        [Required, DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        public string? Gender { get; set; }
        public int RoleId { get; set; }
        [Required, DataType(DataType.MultilineText)]
        public string? Address { get; set; }
    }
}
