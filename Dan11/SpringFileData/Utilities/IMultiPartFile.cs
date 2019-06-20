using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpringFileData.Utilities
{
    // https://docs.spring.io/spring/docs/current/javadoc-api/org/springframework/web/multipart/MultipartFile.html
    public interface IMultiPartFile
    {
        // Defaults
        // Resource getResource();
        // Again, we don't have Path!
        // We have VirtualPathUtility in System.Web
        // void TransferTo(Path dest);
    }
}