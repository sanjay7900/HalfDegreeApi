using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BihariJe_WebApp.Models
{
    public class Order
    {
        
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? Status { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double TotolPrice { get; set; }
        public bool IsApproved { set; get; }
        public string? ApprovedBy { get; set; }
        public bool IsDeliverd { get; set; }
        public string? DeliverdBy { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string? Customer { get; set; }
        public string? CityOrVillage { get; set; }
        public string? District { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? MainAddress { get; set; }
    }
}
