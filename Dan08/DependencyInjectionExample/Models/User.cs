using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DependencyInjectionExample.Models
{
    public class User
    {
        private string name;
        private string email;
        private Logger logger;

        public User(string name, string email, Logger logger)
        {
            this.name = name;
            this.email = email;
            this.logger = logger;
        }

        public void IntroduceYourself()
        {
            string message = "My name is " + this.name;

            logger.Log(message);
        }
    }
}