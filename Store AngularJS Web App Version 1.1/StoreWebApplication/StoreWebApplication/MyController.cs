using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeWebApliccatin
{
    public class MyController : ApiController
    {
        // GET api/<controller>
        [RoutePrefix("NewRoute")]
        public class GetAll
        {
            public string FirstName { get; set; }
            public string SecondName { get; set; }
            public SecondGet SecondGet;
        }
        public class SecondGet
        {
            public string Mobile { get; set; }
            public string EmailId { get; set; }
        }
        [Route("firstCall")]
        [HttpPost]
        public string firstCall(HttpRequestMessage request,
            [FromBody] GetAll getAll)
        {
            return "Data Reached";
        }
    }
}