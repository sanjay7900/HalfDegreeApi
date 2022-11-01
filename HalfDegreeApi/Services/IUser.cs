using HalfDegreeApi.Models;

namespace HalfDegreeApi.Services
{
    public interface IUser
    {
        public bool RegisterUser(User user);
        public string LoginUser(string username, string password);
        public string LogoutUser(string username);
       


    }
}
