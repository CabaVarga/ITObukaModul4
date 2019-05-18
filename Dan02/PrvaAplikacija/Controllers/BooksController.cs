using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PrvaAplikacija.Controllers
{
    [RoutePrefix("api/books")]
    public class BooksController : ApiController
    {
        // GET api/books
        [Route("")]
        public IEnumerable<Book> Get() { ... }


        // ignore prefix
        [Route("~/api/authors/{authorId:int}/books")]
        public IEnumerable<Book> GetByAuthor(int authorId) { ...}
    }
}
