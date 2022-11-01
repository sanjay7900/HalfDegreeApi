using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HalfDegreeApi.Data;
using HalfDegreeApi.Models;
using HalfDegreeApi.Mapper;
using AutoMapper;
using System.Collections.Specialized;
using HalfDegreeApi.Services;
using Microsoft.AspNetCore.Authorization;
using MessagePack.Formatters;
using Newtonsoft.Json;

namespace HalfDegreeApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly HalfDegreeApiDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        private string folder = "Upload/Images/";
        private IProduct<Product> _product;
        
        private  IUser _user;
        private readonly string baseaddress = "https://localhost:7242/";

        public ProductsController(HalfDegreeApiDbContext context,IMapper mapper,IConfiguration configuration,IWebHostEnvironment hostEnvironment,IUser user,IProduct<Product> product)
        {
            _product=product;
            _context = context;
            _mapper = mapper;  
            _configuration = configuration; 
            _env = hostEnvironment;
            
            _user = user;
        }

        // GET: api/Products
       // [Authorize(Roles="Admin")]
        [HttpGet]
        //[Authorize(Roles = "Admin,Customer")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Product>>> Getproducts()
        {
            var products = _product.GetProducts(); //await _context.products!.ToListAsync();
            //foreach(var product in products)
            //{
            //    product.ImageName = baseaddress + product.ImageName;

            //}
            return products;
        }
        
        // GET: api/Products/5
        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = _product.GetProduct(id);// await _context.products!.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            product.ImageName=baseaddress + product.ImageName;  
            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutProduct([FromForm] ImageMapper product)
        {
            

            //_context.Entry(product).State = EntityState.Modified;

            try
            {
                folder += Guid.NewGuid().ToString() + product.ImageName!.FileName;
                string imageForWebApiSide = folder;
                string serverFolder = Path.Combine(_env.WebRootPath, folder);
                await product.ImageName.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

                var pro = _mapper.Map<Product>(product);
                pro.ImageName = folder;
                _product.UpdateProduct(pro);
                // _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> PostProduct([FromForm]ImageMapper product)
        { 
            folder += Guid.NewGuid().ToString() + product.ImageName!.FileName;
            string imageForWebApiSide = folder;
            string serverFolder = Path.Combine(_env.WebRootPath, folder);
            await product.ImageName.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            
            var pro = _mapper.Map<Product>(product);
            pro.ImageName = folder;
            var status=_product.AddProduct(pro);
            if (status)
            {
                return Ok();
            }


            return BadRequest();
        }

        // DELETE: api/Products/5
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = _product.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

           var status=_product.RemoveProduct(id);
            if (status)
            {
                return Ok();
            }

            return BadRequest();
        }



        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        public  async Task<IActionResult> IsAvilable(string name)
        {
            if (_product.IsProductAvailable(name))
            {
                return Ok();
            }
            return NotFound();
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ActionResult<Product>> GetproductName(string name)
        {
            if (_product.IsProductAvailable(name))
            {
                var pro= _product.GetProductByName(name);
                pro.ImageName=baseaddress+pro.ImageName;
                return pro; 
            }
            return NotFound();    
        }

       
    }
}
