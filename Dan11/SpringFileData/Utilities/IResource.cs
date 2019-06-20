using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SpringFileData.Utilities
{
    // https://docs.spring.io/spring/docs/current/javadoc-api/org/springframework/core/io/Resource.html
    public interface IResource
    {
        // REMEMBER: Java Interfaces are unlike .Net interfaces
        // They are more like abstract classes, in that they can have
        // both abstract (not implemented) and non-abstract (implemented) methods
        // non-abstract methods in abstract classes / interfaces are 'default' methods

        // Default methods:
        bool IsFile();
        bool IsOpen();
        bool IsReadable();
        // ReadableByteChannel readableChannel();
        // InputStream getInputStream();

        // Other methods
        long ContentLength();
        // Resource createRelative(string relativePath);
        bool Exists();
        string GetDescription();
        File GetFile();
        string GetFileName();
        Uri GetUri();
        // There is a Url property in the HttpRequest, returns an URI
        // no separate URL class in .Net
        // Url GetUrl();
        long LastModified();
    }
}