using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Controllers
{
    [Route("[Controller]")]
    public class AboutController
    {
        [Route("")]
        public string Phone()
        {
            return "916-555-5555";
        }
        [Route("[Action]")]
        public string Country()
        {
            return "USA";
        }
    }
}
