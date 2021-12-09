using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiapplication.Controllers
{
    public class DemoController : ApiController
    {
        // GET: api/Demo
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Demo/5
        public string Get(int id)
        {
            return "value " + id;
        }

        // POST: api/Demo
        public string Post(string value)
        {
            return "This is POST " + value;
        }

        // PUT: api/Demo/5
        public string Put(int id, string value)
        {
            return "This is PUT " + id;
        }

        // DELETE: api/Demo/5
        public string Delete(int id)
        {
            return "This is Delete " + id;
        }
    }
}
