namespace HalfDegreeApi.Services
{
    public interface IRole<T>
    {
        public bool AddRole(T role);    
        public bool RemoveRole(int role);
        public bool RemoveRole(string role);
        public T GetRole(int roleid);
        public T GetRole(string rolename);
        public T UpdateRole(T role);
        public bool IsAvailable(string rolename);
        public List<T> GetRoles();  

    }
}
