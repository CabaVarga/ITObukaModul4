using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FileUploadAPI.Controllers
{
    public class UploadController : ApiController
    {
        /*
        [Route("api/upload")]
        [HttpPost]
        public IHttpActionResult PostNewFile()
        {

        }
        */

        public async Task<HttpResponseMessage> PostFormData()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");

            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);
                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    Trace.WriteLine("Server file path: " + file.LocalFileName);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [Route("api/upload/gif-payload")]
        [HttpPost]
        public async Task<HttpResponseMessage> PostGifFilesDirectly()
        {
            // Check if the request contains multipart/form-data.

            // Request.Content.Headers.ContentType !=
            string root = HttpContext.Current.Server.MapPath("~/App_Data");

            try
            {
                // Read the form data.
                byte[] payload = await Request.Content.ReadAsByteArrayAsync();

                // we get a stream, where to save?



                // var payload = await Request.Content.ReadAsStreamAsync();
                // StreamReader sr = new StreamReader(payload);
                // BinaryReader br = new BinaryReader(payload);

                // BinaryWriter bw = new BinaryWriter(File.OpenWrite(root + "/file.out"));
                // StreamWriter sw = new StreamWriter(File.OpenWrite(root));

                using (FileStream fs = new FileStream(root + "/file.out", FileMode.Create))
                {
                    await fs.WriteAsync(payload, 0, payload.Length);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }

            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [Route("api/download/{filename}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetUploadedFileByFilename(string filename)
        {
            // 1. Build the path
            string root = HttpContext.Current.Server.MapPath("~/App_Data");

            // 2. Add the filename to the path
            root += "/" + filename;

            // 3. Check if file exists
            if (!File.Exists(root))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            // 4. Start building the multipart message
            var response = Request.CreateResponse();
            var messsage = new MultipartFormDataContent();
            messsage.Add(
                new StreamContent(File.OpenRead(root)));

            //
            // temporary
            return Request.CreateResponse(HttpStatusCode.OK);

        }

        // FROM WEB API 2 Recipes, page 117+
        [Route("api/files/v1/{name}/")]
        public IHttpActionResult GetFileByNameV1(string name)
        {
            var path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data"), name);
            if (!File.Exists(path))
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found"));
            }

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(path, FileMode.Open);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline")
            {
                FileName = name
            };
            return ResponseMessage(result);
        }


    }
}
