using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Project_3rd_clean.Models
{
    public class ContextSeeder : DropCreateDatabaseAlways<DataAccessContext>
    {
        protected override void Seed(DataAccessContext context)
        {
            // USERS
            IList<User> users = new List<User>();
            users.Add(new User() { id = 1, first_name = "Mladen", last_name = "Mladenovic", username = "mladen", password = "lozinka", email = "mladen@mail.com", user_role = User.UserRoles.ROLE_CUSTOMER });
            users.Add(new User() { id = 2, first_name = "Ivan", last_name = "Ivanovic", username = "ivan", password = "lozinka", email = "ivan@mail.com", user_role = User.UserRoles.ROLE_CUSTOMER });
            users.Add(new User() { id = 3, first_name = "Caba", last_name = "Varga", username = "caba", password = "lozinka", email = "caba.varga@gmail.com", user_role = User.UserRoles.ROLE_CUSTOMER });
            users.Add(new User() { id = 4, first_name = "Dragana", last_name = "Tomic", username = "dragan", password = "lozinka", email = "dragana@mail.com", user_role = User.UserRoles.ROLE_CUSTOMER });
            users.Add(new User() { id = 5, first_name = "Dusan", last_name = "Rodic", username = "dusan", password = "lozinka", email = "dusan@mail.com", user_role = User.UserRoles.ROLE_CUSTOMER });
            context.userModels.AddRange(users);

            // CATEGORIES
            IList<Category> categories = new List<Category>();
            categories.Add(new Category() { id = 1, category_name = "Movies", category_description = "a desc" });
            categories.Add(new Category() { id = 2, category_name = "Video Games", category_description = "b desc" });
            categories.Add(new Category() { id = 3, category_name = "Books", category_description = "c desc" });

            context.categoryModels.AddRange(categories);

            // OFFERS
            IList<Offer> offers = new List<Offer>();
            offers.Add(new Offer() { id = 1, offer_name = "Basket", offer_description = "Basket description", regular_price = 10.0M, action_price = 8.0M, available_offers = 10, bought_offers = 5, offer_created = new DateTime(2019, 5, 1), offer_expires = new DateTime(2019, 6, 1), offer_status = Offer.OfferStatus.WAIT_FOR_APPROVING, sellerId = 1, categoryId = 1 });
            offers.Add(new Offer() { id = 2, offer_name = "Chair", offer_description = "Chair description", regular_price = 15.0M, action_price = 13.0M, available_offers = 10, bought_offers = 5, offer_created = new DateTime(2019, 4, 1), offer_expires = new DateTime(2019, 6, 1), offer_status = Offer.OfferStatus.WAIT_FOR_APPROVING, sellerId = 3, categoryId = 3 });
            offers.Add(new Offer() { id = 3, offer_name = "Table", offer_description = "Table description", regular_price = 20.0M, action_price = 15.0M, available_offers = 10, bought_offers = 5, offer_created = new DateTime(2019, 3, 1), offer_expires = new DateTime(2019, 5, 1), offer_status = Offer.OfferStatus.WAIT_FOR_APPROVING, sellerId = 2, categoryId = 1 });
            offers.Add(new Offer() { id = 4, offer_name = "Trousers", offer_description = "Trousers description", regular_price = 20.0M, action_price = 8.0M, available_offers = 10, bought_offers = 5, offer_created = new DateTime(2019, 1, 1), offer_expires = new DateTime(2019, 6, 1), offer_status = Offer.OfferStatus.WAIT_FOR_APPROVING, sellerId = 2, categoryId = 2 });
            offers.Add(new Offer() { id = 5, offer_name = "Keyboard", offer_description = "Keyboard description", regular_price = 5.0M, action_price = 3.0M, available_offers = 10, bought_offers = 5, offer_created = new DateTime(2019, 2, 1), offer_expires = new DateTime(2019, 4, 1), offer_status = Offer.OfferStatus.WAIT_FOR_APPROVING, sellerId = 2, categoryId = 1 });

            context.offerModels.AddRange(offers);

            // BILLS
            IList<Bill> bills = new List<Bill>();
            bills.Add(new Bill() { userId = 3, offerId = 1, billCreated = new DateTime(2019, 7, 1) });
            bills.Add(new Bill() { userId = 3, offerId = 1, billCreated = new DateTime(2019, 7, 2) });
            bills.Add(new Bill() { userId = 3, offerId = 2, billCreated = new DateTime(2019, 7, 2) });
            bills.Add(new Bill() { userId = 3, offerId = 4, billCreated = new DateTime(2019, 7, 1) });
            bills.Add(new Bill() { userId = 1, offerId = 2, billCreated = new DateTime(2019, 7, 3) });
            bills.Add(new Bill() { userId = 2, offerId = 1, billCreated = new DateTime(2019, 7, 1) });
            bills.Add(new Bill() { userId = 1, offerId = 1, billCreated = new DateTime(2019, 7, 2) });
            bills.Add(new Bill() { userId = 3, offerId = 4, billCreated = new DateTime(2019, 7, 2) });
            bills.Add(new Bill() { userId = 2, offerId = 1, billCreated = new DateTime(2019, 7, 1) });
            bills.Add(new Bill() { userId = 1, offerId = 2, billCreated = new DateTime(2019, 7, 3) });
            context.billModels.AddRange(bills);

            // VOUCHERS

            base.Seed(context);
        }
    }
}