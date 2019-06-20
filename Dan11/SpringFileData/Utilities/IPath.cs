using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpringFileData.Utilities
{
    // https://docs.oracle.com/javase/8/docs/api/java/nio/file/Path.html?is-external=true
    // related static helper class Paths: https://docs.oracle.com/javase/8/docs/api/java/nio/file/Paths.html
    public interface IPath
    {
        int CompareTo(IPath other);
        bool EndsWith(IPath other);
        bool EndsWith(string other);
        bool Equals(object other);
        IPath GetFileName();
        // FileSystem analogy in .Net? screw Microsoft
        // FileSystem GetFileSystem();
        IPath GetName();
        int GetNameCount();
        IPath GetParent();
        IPath GetRoot();
        int HashCode();
        bool IsAbsolute();
        // Iterator is something like IEnumerable - probably the GetEnumerator method
        // Iterator<Path> Iterator();
        IPath Normalize();

        // and about a dozen other methods

        // Now, it happens that .Net also has many functionalities in System.IO
        // only the logic is a 'bit' different
        // There are many static classes
        // We have:
        //      Directory, DirectoryInfo
        //      DriveInfo, without Drive
        //      File, FileInfo
        //      FileSystemInfo, no FileSystem
        //      Path, no PathInfo


        // We also have FileSystemWatcher
    }
}