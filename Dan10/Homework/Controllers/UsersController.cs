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

        #region Functionality for 1.1 - 1.4
        [Route("api/users")]
        public IHttpActionResult GetUsers()
        {
            return Ok(usersService.GetAllUsers());
        }

        [Route("api/users/{id}")]
        public IHttpActionResult GetUserByID(int id)
        {
            User user = usersService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [Route("api/users")]
        [HttpPost]
        public IHttpActionResult PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User createdUser = usersService.CreateUser(user);

            if (createdUser == null)
            {
                return BadRequest("Could not create user. Invalid email");
            }

            return Ok(createdUser);
        }

        [Route("api/users/{id}")]
        [HttpPut]
        public IHttpActionResult PostUser(int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            User updatedUser = usersService.UpdateUser(id, user.Name, user.Email, user.City);

            if (updatedUser == null)
            {
                return BadRequest("Could not create user. Invalid email");
            }

            return Ok(updatedUser);
        }

        #endregion

        #region Zadatak 1.1
        /// <summary>
        /// It will create new users and save them to the database, based on the file provided by the user.
        /// The file should be an ordinary textual file, with the following structure:
        /// username,email
        /// 
        /// The request should be of "multipart/form-data" type
        /// 
        /// </summary>
        /// <returns>OK HTTP Status code, empty payload</returns>
        [Route("api/users/from-file")]
        [HttpPost]
        public async Task<HttpResponseMessage> PostUsersFromFile()
        {
            Dictionary<string, string> responseMessage = new Dictionary<string, string>();
            IEnumerable<User> loadedUsers = new List<User>();

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

                    // ZADATAK 1.2:
                    // TODO Add some helper message to the response, based on the output
                    // Something like "23 new Users successfully loaded into database"
                    // Loading failed for x users, check the logs for the details
                    loadedUsers = usersService.CreateNewUsersFromFile(file.LocalFileName);
                }

                responseMessage.Add("Result",
                    String.Format("{0} users successfully loaded into the database", loadedUsers.Count()));

                return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
            }

            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
        #endregion

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
        #region Zadatak 2

        #endregion

    }
}
