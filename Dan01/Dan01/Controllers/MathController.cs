using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dan01.Controllers
{
    public class MathController : ApiController
    {
        // GET: /math/array
        [Route("math/array")]
        [HttpGet]
        public int[] GetArray()
        {
            Random rd = new Random();
            int length = rd.Next(3, 30);
            int[] array = new int[length];
            
            for (int i = 0; i < length; i++)
            {
                array[i] = rd.Next(3, 30);
            }
            return array;
        }

        // GET: /math/array/5
        [Route("math/array/{length}")]
        [HttpGet]
        public int[] GetArray(int length)
        {
            int[] array = new int[length];
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                array[i] = rd.Next(3, 30);
            }
            return array;
        }

        // The following two methods introduce a dilemma
        // Is it OK to use GET with a non-empty body or better to use POST
        // Neither are semantically clean, btw...
        // The old school approach would be to parameterize the url and use a GET
        // https://developer.mozilla.org/en-US/docs/Web/HTTP/Methods/GET (request has body : NO)
        // https://developer.mozilla.org/en-US/docs/Web/HTTP/Methods/POST

        // POST: /math/sortarray
        [Route("math/sortarray")]
        [HttpPost]
        public int[] SortArray([FromBody]int[] incoming)
        {
            Array.Sort(incoming);

            return incoming;            
        }

        // POST: /math/minmax
        [Route("math/minmax")]
        [HttpPost]
        public Dictionary<string, int> FindMinMax([FromBody]int[] incoming)
        {
            int min = incoming.Min();
            int max = incoming.Max();

            return new Dictionary<string, int> { ["min"] = min, ["max"] = max };
        }
    }
}
