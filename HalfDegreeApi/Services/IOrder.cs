using HalfDegreeApi.Models;

namespace HalfDegreeApi.Services
{
    public interface IOrder<T>
    {
        public bool  AddOrder(T Odered );
        public bool IsApproved(int orderId ,string adminid);
        public bool IsDelivered(int orderId,string deliveredpersonid);
        public List<T> GetOrders();
        public T GetOrder(int orderId);
        public List<MyOrderResponse> GetMyOrders(string customerId);
        public List<T> ApprovedOrders();


    }
}
