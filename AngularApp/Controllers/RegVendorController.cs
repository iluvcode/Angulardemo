using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GoldenPalm.BL;
using GoldenPalm.DAL;


using System.IO;



namespace AP11.Controllers
{
     [AllowCrossSiteJsonAttribute]
    public class RegVendorController : ApiController
    {
         repositoryNom rep = new repositoryNom();

        // GET: api/RegVendor
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/RegVendor/5
          [System.Web.Http.AcceptVerbs("GET", "POST")]
       [HttpGet]
        public List<dtoNomination> GetNominations(string keyword)
        {
            return rep.getNominations();
        }

        // POST: api/RegVendor
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/RegVendor/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/RegVendor/5
        public void Delete(int id)
        {
        }
    }
}
