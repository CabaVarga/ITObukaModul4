using Homework.Models;
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
        private IFileResourcesService fileResourcesService;

        public UsersController(IUsersService usersService, IFileResourcesService fileResourcesService)
        {
            this.usersService = usersService;
            this.fileResourcesService = fileResourcesService;
        }

        /// <summary>
        /// It will create new users and save them to the database, based on the file provided by the user.
        /// The file should be an ordinary textual file, with the following structure:
        /// username,email
        /// 
        /// The request should be of "multipart/form-data" type
        /// 
        /// </summary>
        /// <returns>OK HTTP Status code, empty payload</returns>
        [Route("api/users")]
        [HttpPost]
        public async Task<HttpResponseMessage> PostUsersFromFile()
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
                    Trace.WriteLine("Server file path: " + file.LocalFileName);

                    // FULLY LOAD FILE, provide UsersService method with localFileName as argument,
                    // Let the service do all the work...
                    // Maybe connect with Service here?
                    usersService.CreateNewUsersFromFile(file.LocalFileName);

                    Trace.WriteLine("Users created successfully");
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }

            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }





        // borko
        [Route("api/borko")]
        [HttpPost]
        public async Task<HttpResponseMessage> PostFormData()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            string root = HttpContext.Current.Server.MapPath("~/App_Data");//gde se cuva fajl na serveru
            var provider = new MultipartFormDataStreamProvider(root);//klasa koja daje string do foldera gde se fajl smesta i dodeljuje novo ime fajlu
            string serverpath = null;
            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);
                // This illustrates how to get the file names. //ovo treba staviti u service sloj i odatle se dobija gde je fajl sacuvan i kako se zove
                foreach (MultipartFileData file in provider.FileData)
                {
                    serverpath = file.LocalFileName;
                }

                // IEnumerable<User> usersFromFile = usersService.CreateNewUsersFromFile(serverpath);
                IEnumerable<User> usersFromFile = usersService.GetUsersFromFile(serverpath);

                foreach (var user in usersFromFile)
                {
                    Trace.WriteLine(user.Name);
                    usersService.CreateUser(user);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }


        /// <summary>
        /// Variation of the exercise from the class.
        /// We need to connect the file with the given user as an owner.
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>Status code with empty payload.</returns>
        [Route("api/users/{id}/upload")]
        [HttpPost]
        public async Task<HttpResponseMessage> UploadFileForUser(int id)
        {
            // Check if user exists
            User owner = usersService.GetUser(id);
            if (owner == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

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

                    // FileResource creation logic comes here: (probably not optimal)
                    FileResource fileResource = new FileResource()
                    {
                        User = owner,
                        Description = "Some random file",
                        Path = file.LocalFileName
                    };

                    // If a user can create a FileResource, should I add that capability to the UsersService?

                    fileResourcesService.CreateFileResource(fileResource); // probably need method with parameters

                }
                var returnMessage = Request.CreateResponse(HttpStatusCode.OK);
                // We need to add info about the newly created resource...
                // By using the CreatedAtRoute() method, probably

                return returnMessage;   // Request.CreateResponse(HttpStatusCode.OK);
            }

            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

    }
}
