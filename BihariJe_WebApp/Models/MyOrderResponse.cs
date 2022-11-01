namespace BihariJe_WebApp.Models
{
    public class MyOrderResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double TotolPrice { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsDeliverd { get; set; }
        public bool IsApproved { get; set; }
        public string? Customer { get; set; }
        public string? MainAddress { get; set; }
    }
}
