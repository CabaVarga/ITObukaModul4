using Homework.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Homework.Services
{
    public interface IFileResourcesService
    {
        IEnumerable<FileResource> GetAllFileResources();

        FileResource GetFileResource(int id);

        FileResource CreateFileResource(FileResource fileResource);

        FileResource UpdateFileResource(int id, User user, string description, string path);

        FileResource DeleteFileResource(int id);

        //         IEnumerable<User> CreateNewUsersFromFileResource(string path); 

        // FILE DATA RETRIEVAL
        byte[] RetrieveFileResourceDataAsByteArray(FileResource fileResource);
        byte[] RetrieveFileResourceDataAsByteArray(int id);
        FileStream RetrieveDataAsFileStream(FileResource fileResource);
        FileStream RetrieveDataAsFileStream(int id);

        // FILE DATA WRITE
        // int WriteFileResourceDataAsByteArray(string path);


        // FILE DATA UPDATE (NEW DATA)


        // FILE DATA DELETE
    }
}