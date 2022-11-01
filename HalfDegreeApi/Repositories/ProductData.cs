using HalfDegreeApi.Data;
using HalfDegreeApi.Models;
using HalfDegreeApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace HalfDegreeApi.Repositories
{
    public class ProductData : IProduct<Product>
    {
        private readonly HalfDegreeApiDbContext _context;
        private readonly IWebHostEnvironment _hostEviornment;
        private readonly string url = "https://localhost:7242/";

        public ProductData(HalfDegreeApiDbContext halfDegreeApiDbContext,IWebHostEnvironment webHostEnvironment)
        {
            _context = halfDegreeApiDbContext;
            _hostEviornment = webHostEnvironment;

        }
        public bool AddProduct(Product product)
        {
            try
            {
                if (!IsProductAvailable(product.Name))
                {
                    _context.products!.Add(product);
                    _context.SaveChanges();

                    return true;    
                }
                else
                {
                    return false;

                }
            }
            catch(Exception e)
            {
                return false;
            }
            //throw new NotImplementedException();
        }

        public Product GetProduct(int id)
        {
            try
            {

                var isavailable = _context.products!.FirstOrDefault(p => p.Id == id);
                if (isavailable != null)
                {
                    return isavailable; 
                }
            }
            catch(Exception ex)
            {


            }
            throw new NotImplementedException();
        }

        public Product GetProductByName(string name)
        {
            try
            {
                var pro=_context.products!.FirstOrDefault(p => p.Name == name)!;
                return pro;
            }
            catch(Exception ex)
            {
                return new Product();
            }
        }

        public List<Product> GetProducts()
        {
            var products= _context.products!.ToList();
            foreach(var product in products)
            {
                product.ImageName=url+product.ImageName;

            }
            return products;
        }

        public bool IsProductAvailable(string name)
        {
            try
            {
                if (_context.products!.Any(x => x.Name == name))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveProduct(int id)
        {
            try
            {
                var removeproduct= GetProduct(id);
                if(removeproduct != null)
                {
                    var imagePath = Path.Combine(_hostEviornment.WebRootPath, removeproduct.ImageName);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                       
                    _context.products!.Remove(removeproduct);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;

                }
                
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;

            _context.products!.Update(product);
            _context.SaveChanges();

            return true;
        }
    }
}
