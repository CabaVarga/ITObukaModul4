using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Dan07.Models
{
    public class UsersDBInitializer : DropCreateDatabaseAlways<UserContext>
    {
        protected override void Seed(UserContext context)
        {
            IList<User> users = new List<User>();
            IList<Address> addresses = new List<Address>();

            addresses.Add(new Address() { AddressID = 1, Street = "Beogradska", Number = 43, City = "Novi Sad", Country = "Finland" });
            addresses.Add(new Address() { AddressID = 2, Street = "Beogradska", Number = 45, City = "Novi Sad", Country = "Denmark" });
            addresses.Add(new Address() { AddressID = 3, Street = "Novosadska", Number = 47, City = "Beograd", Country = "Serbia" });
            addresses.Add(new Address() { AddressID = 4, Street = "Strazilovska", Number = 34, City = "Beograd", Country = "Costa Rica" });
            addresses.Add(new Address() { AddressID = 5, Street = "Rumenacka", Number = 78, City = "Beograd", Country = "Serbia" });
            addresses.Add(new Address() { AddressID = 6, Street = "Temerinska", Number = 80, City = "Beograd", Country = "Bangladesh" });
            addresses.Add(new Address() { AddressID = 7, Street = "Beogradska", Number = 81, City = "Novi Sad", Country = "Equador" });
            addresses.Add(new Address() { AddressID = 8, Street = "Strazilovska", Number = 90, City = "Beograd", Country = "Serbia" });
            addresses.Add(new Address() { AddressID = 9, Street = "Radnicka", Number = 98, City = "Beograd", Country = "Australia" });
            addresses.Add(new Address() { AddressID = 10, Street = "Novosadska", Number = 12, City = "Temerin", Country = "Greece" });
            addresses.Add(new Address() { AddressID = 11, Street = "Beogradska", Number = 11, City = "Beograd", Country = "Serbia" });

            context.Addresses.AddRange(addresses);

            users.Add(new User() { UserID = 1, Name = "Marko", Email = "marko@gmail.com", AddressID = 1 });
            users.Add(new User() { UserID = 2, Name = "Milan", Email = "milan@gmail.com", AddressID = 2 });
            users.Add(new User() { UserID = 3, Name = "Dejan", Email = "dejan@gmail.com", AddressID = 3 });
            users.Add(new User() { UserID = 4, Name = "Milica", Email = "milica@gmail.com", AddressID = 4 });
            users.Add(new User() { UserID = 5, Name = "Sladjana", Email = "sladjana@gmail.com", AddressID = 5 });
            users.Add(new User() { UserID = 6, Name = "Slavica", Email = "slavica@gmail.com", AddressID = 6 });
            users.Add(new User() { UserID = 7, Name = "Danilo", Email = "danilo@gmail.com", AddressID = 7 });
            users.Add(new User() { UserID = 8, Name = "Ivan", Email = "ivan@gmail.com", AddressID = 8 });
            users.Add(new User() { UserID = 9, Name = "Ivana", Email = "ivana@gmail.com", AddressID = 9 });
            users.Add(new User() { UserID = 10, Name = "Robert", Email = "robert@gmail.com", AddressID = 10 });

            context.Users.AddRange(users);

            base.Seed(context);
        }
    }
}