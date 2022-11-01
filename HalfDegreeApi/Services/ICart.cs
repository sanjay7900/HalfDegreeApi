using HalfDegreeApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HalfDegreeApi.Services
{
    public interface ICart
    {
        public IActionResult AddCart(Cart cart);

        
    }
}
