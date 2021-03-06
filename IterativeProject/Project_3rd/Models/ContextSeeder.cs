﻿using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Project_3rd.Models
{
    public class ContextSeeder : DropCreateDatabaseAlways<DataAccessContext>
    {
        protected override void Seed(DataAccessContext context)
        {
            // USERS
            IList<UserModel> users = new List<UserModel>();
            users.Add(new UserModel() { id = 1, first_name = "Mladen", last_name = "Mladenovic", username = "mladen", password = "lozinka", email = "mladen@mail.com", user_role = UserModel.UserRoles.ROLE_CUSTOMER });
            users.Add(new UserModel() { id = 2, first_name = "Ivan", last_name = "Ivanovic", username = "ivan", password = "lozinka", email = "ivan@mail.com", user_role = UserModel.UserRoles.ROLE_CUSTOMER });
            users.Add(new UserModel() { id = 3, first_name = "Caba", last_name = "Varga", username = "caba", password = "lozinka", email = "caba.varga@gmail.com", user_role = UserModel.UserRoles.ROLE_CUSTOMER });
            users.Add(new UserModel() { id = 4, first_name = "Dragana", last_name = "Tomic", username = "dragan", password = "lozinka", email = "dragana@mail.com", user_role = UserModel.UserRoles.ROLE_CUSTOMER });
            users.Add(new UserModel() { id = 5, first_name = "Dusan", last_name = "Rodic", username = "dusan", password = "lozinka", email = "dusan@mail.com", user_role = UserModel.UserRoles.ROLE_CUSTOMER });
            context.userModels.AddRange(users);

            // CATEGORIES
            IList<CategoryModel> categories = new List<CategoryModel>();
            categories.Add(new CategoryModel() { id = 1, category_name = "a", category_description = "a desc" });
            categories.Add(new CategoryModel() { id = 2, category_name = "b", category_description = "b desc" });
            categories.Add(new CategoryModel() { id = 3, category_name = "c", category_description = "c desc" });

            context.categoryModels.AddRange(categories);

            // OFFERS
            IList<OfferModel> offers = new List<OfferModel>();
            offers.Add(new OfferModel() { offer_name = "Basket", offer_description = "Basket description", regular_price = 10.0M, action_price = 8.0M, available_offers = 10, bought_offers = 5, offer_created = new DateTime(2019, 5, 1), offer_expires = new DateTime(2019, 6, 1), offer_status = OfferModel.OfferStatus.WAIT_FOR_APPROVING, sellerId = 1, categoryId = 1 });
            offers.Add(new OfferModel() { offer_name = "Chair", offer_description = "Chair description", regular_price = 15.0M, action_price = 13.0M, available_offers = 10, bought_offers = 5, offer_created = new DateTime(2019, 4, 1), offer_expires = new DateTime(2019, 6, 1), offer_status = OfferModel.OfferStatus.WAIT_FOR_APPROVING, sellerId = 3, categoryId = 3 });
            offers.Add(new OfferModel() { offer_name = "Table", offer_description = "Table description", regular_price = 20.0M, action_price = 15.0M, available_offers = 10, bought_offers = 5, offer_created = new DateTime(2019, 3, 1), offer_expires = new DateTime(2019, 5, 1), offer_status = OfferModel.OfferStatus.WAIT_FOR_APPROVING, sellerId = 2, categoryId = 1 });
            offers.Add(new OfferModel() { offer_name = "Trousers", offer_description = "Trousers description", regular_price = 20.0M, action_price = 8.0M, available_offers = 10, bought_offers = 5, offer_created = new DateTime(2019, 1, 1), offer_expires = new DateTime(2019, 6, 1), offer_status = OfferModel.OfferStatus.WAIT_FOR_APPROVING, sellerId = 2, categoryId = 2 });
            offers.Add(new OfferModel() { offer_name = "Keyboard", offer_description = "Keyboard description", regular_price = 5.0M, action_price = 3.0M, available_offers = 10, bought_offers = 5, offer_created = new DateTime(2019, 2, 1), offer_expires = new DateTime(2019, 4, 1), offer_status = OfferModel.OfferStatus.WAIT_FOR_APPROVING, sellerId = 2, categoryId = 1 });

            context.offerModels.AddRange(offers);

            // BILLS
            IList<BillModel> bills = new List<BillModel>();
            bills.Add(new BillModel() { userId = 3, offerId = 1 });

            // VOUCHERS

            base.Seed(context);
        }
    }
}