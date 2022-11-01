using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BihariJe_WebApp.Data;
using BihariJe_WebApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using BihariJe_WebApp.ApiRepositories;

namespace BihariJe_WebApp.Controllers
{
    public class ImageMappersController : Controller
    {
        private readonly BihariJe_WebAppContext _context;

        public ImageMappersController(BihariJe_WebAppContext context)
        {
            _context = context;
        }

        // GET: ImageMappers
        public async Task<IActionResult> Index()
        {
              return View(await _context.ImageMapper.ToListAsync());
        }
        public IActionResult odata()
        {
            //var claims = (System.Security.Claims.ClaimsIdentity)Context.User.Identity;
            //var role = claims.Where(r => r.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();
            ViewData["st"]=User.FindFirstValue(ClaimTypes.Role);
            return View();
        }

        // GET: ImageMappers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ImageMapper == null)
            {
                return NotFound();
            }

            var imageMapper = await _context.ImageMapper
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imageMapper == null)
            {
                return NotFound();
            }

            return View(imageMapper);
        }

        // GET: ImageMappers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ImageMappers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm]ImageMapper imageMapper)
        {
            if (ModelState.IsValid)
            {
                new AdminSite(HttpContext.Session.GetString("JwtToken")).AddProduct(imageMapper);
                return RedirectToAction("Index","Home",new {area=""});
            }
            return View(imageMapper);
        }

        // GET: ImageMappers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ImageMapper == null)
            {
                return NotFound();
            }

            var imageMapper = await _context.ImageMapper.FindAsync(id);
            if (imageMapper == null)
            {
                return NotFound();
            }
            return View(imageMapper);
        }

        // POST: ImageMappers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Quantity")] ImageMapper imageMapper)
        {
            if (id != imageMapper.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imageMapper);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageMapperExists(imageMapper.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(imageMapper);
        }

        // GET: ImageMappers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ImageMapper == null)
            {
                return NotFound();
            }

            var imageMapper = await _context.ImageMapper
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imageMapper == null)
            {
                return NotFound();
            }

            return View(imageMapper);
        }

        // POST: ImageMappers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ImageMapper == null)
            {
                return Problem("Entity set 'BihariJe_WebAppContext.ImageMapper'  is null.");
            }
            var imageMapper = await _context.ImageMapper.FindAsync(id);
            if (imageMapper != null)
            {
                _context.ImageMapper.Remove(imageMapper);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageMapperExists(int id)
        {
          return _context.ImageMapper.Any(e => e.Id == id);
        }
        public async Task<IActionResult>  Logout()
        {
            HttpContext.Session.Clear();
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(odata));
        }
    }
}
