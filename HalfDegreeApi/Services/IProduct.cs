namespace HalfDegreeApi.Services
{
    public interface IProduct<T>
    {
        public List<T> GetProducts();
        public T GetProduct(int id);
        public bool IsProductAvailable(string name);
        public bool AddProduct(T product);  
        public bool RemoveProduct(int id);
        public T GetProductByName(string name);
        public bool UpdateProduct(T product);


    }
}
