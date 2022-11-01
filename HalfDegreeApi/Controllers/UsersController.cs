using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HalfDegreeApi.Data;
using HalfDegreeApi.Models;
using HalfDegreeApi.Services;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace HalfDegreeApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly HalfDegreeApiDbContext _context;
        private IUser _usercontext;

        public UsersController(HalfDegreeApiDbContext context,IUser user)
        {
            _context = context;
            _usercontext = user;

        }

        // GET: api/Users
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<User>>> Getusers()
        {
            return await _context.users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _usercontext.RegisterUser(user);

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult LoginUser(string Emailid,string password)
        {
          var check= _usercontext.LoginUser(Emailid,password);  
            if(check== "UnAuthentication")
            {
                return BadRequest();
            }
            else
            {
                return Ok(check);
            }
        }

        private bool UserExists(int id)
        {
            return _context.users.Any(e => e.Id == id);
        }
    }
}
