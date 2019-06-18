using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework.Models
{
    public class FileResource
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }

        public virtual User User { get; set; }
    }
}