namespace DIExample
{
    public class User
    {
        private string name;
        private string email;
        private ILogger logger;

        public User(string name, string email, ILogger logger)
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
