using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BihariJe_WebApp.Models;

namespace BihariJe_WebApp.Data
{
    public class BihariJe_WebAppContext : DbContext
    {
        public BihariJe_WebAppContext (DbContextOptions<BihariJe_WebAppContext> options)
            : base(options)
        {
        }
        public DbSet<BihariJe_WebApp.Models.ImageMapper> ImageMapper { get; set; }

        public DbSet<BihariJe_WebApp.Models.Product> Product { get; set; } = default!;
    }
}
