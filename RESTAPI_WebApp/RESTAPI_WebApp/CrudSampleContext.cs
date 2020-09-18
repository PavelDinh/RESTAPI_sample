using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAPI_WebApp.Controllers
{
    public class CrudSampleContext : DbContext
    {
        public CrudSampleContext(DbContextOptions<CrudSampleContext> options) : base (options)
        {

        }
        public DbSet<Customer> customers { get; set; }
    }
}
