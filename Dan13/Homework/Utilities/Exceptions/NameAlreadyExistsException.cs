using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework.Utilities.Exceptions
{
    public class NameAlreadyExistsException : Exception
    {
        public NameAlreadyExistsException(string message) : base(message)
        {
        }
    }
}