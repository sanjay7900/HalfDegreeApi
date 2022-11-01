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
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace HalfDegreeApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private IRole<Roles> _context;

        public RolesController(IRole<Roles> context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Getroles()
        {
            return Ok(JsonConvert.SerializeObject(_context.GetRoles()));
        }

        // GET: api/Roles/5
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Roles>> GetRole(int id)
        {
            var roles =  _context.GetRole(id);

            if (roles == null)
            {
                return NotFound();
            }

            return Ok(JsonConvert.SerializeObject(roles));
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutRoles(Roles roles)
        {
           


            try
            {
                _context.UpdateRole(roles);
                return Ok();
                
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

            return NoContent();
        }

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Roles>> PostRoles(Roles roles)
        {
            _context.AddRole(roles);

            return CreatedAtAction("GetRoles", new { id = roles.Id }, roles);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoles(int id)
        {
           _context.RemoveRole(id);

            return Ok();
        }

        private bool RolesExists(int id)
        {
            return _context.GetRole(id) != null;
        }
    }
}
