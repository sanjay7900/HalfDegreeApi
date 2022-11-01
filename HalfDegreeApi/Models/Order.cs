using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HalfDegreeApi.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required,DataType(DataType.Text)]
        public string? Status { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double TotolPrice { get; set; }
        public bool IsApproved { set; get; }
        [DataType(DataType.EmailAddress)]
        public string? ApprovedBy { get; set; }
        public bool IsDeliverd { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? DeliverdBy { get; set; }
        public DateTime DateTime { get; set; }= DateTime.Now;
        [Required,DataType(DataType.EmailAddress)]
        public string? Customer { get; set; }
        [Required,DataType(DataType.Text)]
        public string? CityOrVillage { get; set; }
        [Required,DataType(DataType.Text)]
        public string? District { get; set; }
        [Required,DataType(DataType.Text)]
        public string? State { get; set; }
        [Required,DataType(DataType.PostalCode)]
        public string? ZipCode { get; set; }
        [Required,DataType(DataType.MultilineText)]
        public string? MainAddress { get; set; }
       


    }
}
