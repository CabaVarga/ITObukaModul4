using Homework.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Homework.Controllers
{
    public class UsersController : ApiController
    {
        private IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [Route("api/users")]
        [HttpPost]
        public async Task<HttpResponseMessage> PostUsersFromFile()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            // DELETE AFTER
            var httpRequest = HttpContext.Current.Request;
            var hfc = httpRequest.Files;
            Trace.WriteLine(string.Format("HttpFilesCollection Count: {0}", hfc.Count));
            Trace.WriteLine(httpRequest.Files);

            



            string root = HttpContext.Current.Server.MapPath("~/App_Data");

            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);
                // This illustrates how to get the file names.

                foreach (MultipartFileData file in provider.FileData)
                {
                    var fh = file.Headers;
                    
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    Trace.WriteLine("HEADERS PROPERTIES:");
                    Trace.Indent();
                    Trace.WriteLine(string.Format("Allow: {0}", fh.Allow));

                    Trace.WriteLine("--- ContentDisposition: ");
                    Trace.Indent();
                    Trace.WriteLine(string.Format("CreationDate: {0}", fh.ContentDisposition.CreationDate));
                    Trace.WriteLine(string.Format("DispositionType: {0}", fh.ContentDisposition.DispositionType));
                    Trace.WriteLine(string.Format("FileName: {0}", fh.ContentDisposition.FileName));
                    Trace.WriteLine(string.Format("FileNameStar: {0}", fh.ContentDisposition.FileNameStar));
                    Trace.WriteLine(string.Format("ModificationDate: {0}", fh.ContentDisposition.ModificationDate));
                    Trace.WriteLine(string.Format("Name: {0}", fh.ContentDisposition.Name));
                    Trace.WriteLine(string.Format("Parameters: {0}", fh.ContentDisposition.Parameters));
                    Trace.WriteLine(string.Format("ReadDate: {0}", fh.ContentDisposition.ReadDate));
                    Trace.WriteLine(string.Format("Size: {0}", fh.ContentDisposition.Size));
                    Trace.Unindent();


                    Trace.WriteLine(string.Format("ContentEncoding: {0}", fh.ContentEncoding));
                    Trace.WriteLine(string.Format("ContentLanguage: {0}", fh.ContentLanguage));

                    // This one throws an exception, I don't know the reason...
                    // Trace.WriteLine(string.Format("ContentLength: {0}", fh.ContentLength));

                    Trace.WriteLine(string.Format("ContentLocation: {0}", fh.ContentLocation));
                    Trace.WriteLine(string.Format("ContentMD5: {0}", fh.ContentMD5));
                    Trace.WriteLine(string.Format("ContentRange: {0}", fh.ContentRange));
                    Trace.WriteLine(string.Format("ContentType: {0}", fh.ContentType));
                    Trace.WriteLine(string.Format("Expires: {0}", fh.Expires));
                    Trace.WriteLine(string.Format("LastModified: {0}", fh.LastModified));


                    Trace.Unindent();

                    
                    Trace.WriteLine("Server file path: " + file.LocalFileName);
                    // New Traces:

                    // Maybe connect with Service here?
                    usersService.CreateNewUsersFromFile(file.LocalFileName);
                    Trace.WriteLine("Users created successfully");
                }

                // What and how to connect with service?

                // OPTION 1:
                // FULLY LOAD FILE, provide UsersService method with localFileName as argument,
                // Let the service do all the work...
                


                return Request.CreateResponse(HttpStatusCode.OK);
            }

            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
