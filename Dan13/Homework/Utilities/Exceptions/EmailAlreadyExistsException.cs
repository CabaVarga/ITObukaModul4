using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework.Utilities.Exceptions
{
    public class EmailAlreadyExistsException : Exception
    {
        public EmailAlreadyExistsException(string message) : base(message)
        {
        }
    }
}