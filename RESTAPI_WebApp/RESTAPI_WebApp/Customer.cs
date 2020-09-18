using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAPI_WebApp.Controllers
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public DateTime VisitDateTime { get; set; }
        public int Age { get; set; }
        public bool WasSatisfied { get; set; }
        public char Sex { get; set; }
    }
}
