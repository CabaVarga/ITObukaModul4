using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Dan07.Models
{
    public class UsersDBInitializer : DropCreateDatabaseIfModelChanges<UserContext>
    {
        protected override void Seed(UserContext context)
        {
            IList<User> users = new List<User>();
            IList<Address> addresses = new List<Address>();

            addresses.Add(new Address() { Street = "Beogradska", Number = 43, City = "Novi Sad", Country = "Finland" });
            addresses.Add(new Address() { Street = "Beogradska", Number = 45, City = "Novi Sad", Country = "Denmark" });
            addresses.Add(new Address() { Street = "Novosadska", Number = 47, City = "Beograd", Country = "Serbia" });
            addresses.Add(new Address() { Street = "Strazilovska", Number = 34, City = "Beograd", Country = "Costa Rica" });
            addresses.Add(new Address() { Street = "Rumenacka", Number = 78, City = "Beograd", Country = "Serbia" });
            addresses.Add(new Address() { Street = "Temerinska", Number = 80, City = "Beograd", Country = "Bangladesh" });
            addresses.Add(new Address() { Street = "Beogradska", Number = 81, City = "Novi Sad", Country = "Equador" });
            addresses.Add(new Address() { Street = "Strazilovska", Number = 90, City = "Beograd", Country = "Serbia" });
            addresses.Add(new Address() { Street = "Radnicka", Number = 98, City = "Beograd", Country = "Australia" });
            addresses.Add(new Address() { Street = "Novosadska", Number = 12, City = "Temerin", Country = "Greece" });
            addresses.Add(new Address() { Street = "Beogradska", Number = 11, City = "Beograd", Country = "Serbia" });

            context.Addresses.AddRange(addresses);

            users.Add(new User() { Name = "Marko", Email = "marko@gmail.com", AddressID = 1 });
            users.Add(new User() { Name = "Milan", Email = "milan@gmail.com", AddressID = 2 });
            users.Add(new User() { Name = "Dejan", Email = "dejan@gmail.com", AddressID = 3 });
            users.Add(new User() { Name = "Milica", Email = "milica@gmail.com", AddressID = 4 });
            users.Add(new User() { Name = "Sladjana", Email = "sladjana@gmail.com", AddressID = 5 });
            users.Add(new User() { Name = "Slavica", Email = "slavica@gmail.com", AddressID = 6 });
            users.Add(new User() { Name = "Danilo", Email = "danilo@gmail.com", AddressID = 7 });
            users.Add(new User() { Name = "Ivan", Email = "ivan@gmail.com", AddressID = 8 });
            users.Add(new User() { Name = "Ivana", Email = "ivana@gmail.com", AddressID = 9 });
            users.Add(new User() { Name = "Robert", Email = "robert@gmail.com", AddressID = 10 });

            context.Users.AddRange(users);

            base.Seed(context);
        }
    }
}