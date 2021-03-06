﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Project_3rd.Models;
using Project_3rd.Repositories;
using Project_3rd.Services;

namespace Project_3rd.Controllers
{
    public class BillController : ApiController
    {
        private IUnitOfWork db;
        private IBillService billService;
        private IOfferService offerService;
        private IUserService userService;
        private IVoucherService voucherService;
        private IEmailsService emailsService;

        public BillController(IUnitOfWork db, IBillService billService, IOfferService offerService, IUserService userService, IVoucherService voucherService, IEmailsService emailsService)
        {
            this.db = db;
            this.billService = billService;
            this.offerService = offerService;
            this.userService = userService;
            this.voucherService = voucherService;
            this.emailsService = emailsService;
        }

        // GET: api/BillModels
        [Route("project/bills")]
        public IEnumerable<BillModel> GetbillModels()
        {
            return billService.GetAllBills();
        }

        // GET: api/BillModels/5
        [Route("project/bills/{id}", Name = "SingleBillById")]
        [ResponseType(typeof(BillModel))]
        public IHttpActionResult GetBillModel(int id)
        {
            BillModel billModel = billService.GetBill(id);

            if (billModel == null)
            {
                return NotFound();
            }

            return Ok(billModel);
        }

        // PUT: api/BillModels/5
        [Route("project/bills/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBillModel(int id, BillModel billModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != billModel.id)
            {
                return BadRequest();
            }

            BillModel billToChange = billService.UpdateBill(id, billModel);

            if (billToChange == null)
            {
                return NotFound();
            }

            // The following two clauses are from the third iteration:
            if (billToChange.paymentMade)
            {
                // TODO vouchersService.CreateVoucher(updatedBill);
                VoucherModel newVoucher = voucherService.CreateVoucherAfterBillPayment(billToChange);

                emailsService.SendEmail(newVoucher);
            }

            if (billToChange.paymentCancelled)
            {
                // The service method I didn't know it's function
                offerService.UpdateOffer(billToChange.offerModel, false);
            }

            return Ok(billToChange);
        }

        // POST: api/BillModels
        [Route("project/bills", Name = "PostBill")]
        [ResponseType(typeof(BillModel))]
        public IHttpActionResult PostBillModel(BillModel billModel)
        {
            //// ModelState is not enough, we need nonempty offerId and buyerId foreign keys too
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            // return CreatedAtRoute("SingleBillById", new { id = billModel.id }, billModel);
            // they are nullable, although the entity will not be created! FIX THE MODEL
            if (!ModelState.IsValid || billModel.offerId == null || billModel.userId == null)
            {
                return BadRequest(ModelState);
            }

            OfferModel offer = offerService.GetOffer((int)billModel.offerId);
            UserModel buyer = userService.GetUser((int)billModel.userId);

            if (offer == null || buyer == null)
            {
                return NotFound();
            }

            if (buyer.user_role != UserModel.UserRoles.ROLE_CUSTOMER)
            {
                return BadRequest("User's role must be ROLE_CUSTOMER");
            }

            billModel.offerModel = offer;
            billModel.userModel = buyer;

            // VAZNO: ovo nije eksplicitno objasnjeno ali sledi iz logike rada
            // Kada je kreiran racun, koji svakako mora biti povezan sa ponudom, moramo azurirati odredjena polja i u ponudi
            offerService.UpdateOffer(offer, true);
            BillModel createdBill = billService.CreateBill(billModel);

            return CreatedAtRoute("PostBill", new { id = createdBill.id }, createdBill);
        }

        // DELETE: api/BillModels/5
        [Route("project/bills/{id}")]
        [ResponseType(typeof(BillModel))]
        public IHttpActionResult DeleteBillModel(int id)
        {
            BillModel billModel = billService.DeleteBill(id);

            if (billModel == null)
            {
                return NotFound();
            }

            return Ok(billModel);
        }

        #region Connect with offer and user -- they shouldn't be distinct methods!
        // ZADATAK 3.6
        // PUT project/bills/4/connect-offer/3
        [Route("project/bills/{billId}/connect-offer/{offerId}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutConnectBillAndOffer(int billId, int offerId)
        {
            BillModel bill = db.BillsRepository.GetByID(billId);

            if (bill == null)
            {
                return NotFound();
            }

            OfferModel offer = db.OffersRepository.GetByID(offerId);

            if (offer == null)
            {
                return NotFound();
            }

            bill.offerModel = offer;
            db.BillsRepository.Update(bill);
            db.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // ZADATAK 3.8
        // PUT project/bills/4/connect-user/3
        [Route("project/bills/{billId}/connect-user/{userId}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutConnectBillAnUser(int billId, int userId)
        {
            BillModel bill = db.BillsRepository.GetByID(billId);

            if (bill == null)
            {
                return NotFound();
            }

            UserModel user = db.UsersRepository.GetByID(userId);

            if (user == null)
            {
                return NotFound();
            }

            if (user.user_role != UserModel.UserRoles.ROLE_CUSTOMER)
            {
                return BadRequest("User is not a customer");
            }

            bill.userModel = user;
            db.BillsRepository.Update(bill);
            db.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }
        #endregion

        // ZADATAK 3.3.9
        // GET project/bills/findByBuyer/{buyerId}
        [Route("project/bills/findByBuyer/{buyerId}")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<BillModel>))]
        public IHttpActionResult GetAllBillsOfUser(int buyerId)
        {
            //UserModel buyer = db.UsersRepository.GetByID(buyerId);

            //if (buyer == null)
            //{
            //    return NotFound();
            //}

            //if (buyer != null)
            //{
            //    Debug.WriteLine("Buyer is found, and it's: {0}, {1}, with id: {2}", buyer.first_name, buyer.last_name, buyerId);
            //}

            //// THIS ONLY WORKED AFTER CHANGED NAVIGATION PROPERTY FROM IENUMERABLE TO ICOLLECTION...
            //// see here: https://stackoverflow.com/a/32997694/4486196
            //IEnumerable<BillModel> bills =
            //    db.UsersRepository.
            //    Get(
            //        filter: u => u.id == buyerId,
            //        includeProperties: "billModels").
            //        FirstOrDefault().
            //        billModels;

            //// For some reason with the following one billModels is always null, with and without virtual (lazy loading) ?
            //// IEnumerable<BillModel> bills = buyer.billModels.ToList();


            ///*
            // * EXTREMELY IMPORTANT: 
            // * From a more specific standpoint, lazy loading comes in to play with choosing the type. 
            // * By default, navigation properties in Entity Framework come with change tracking and are proxies. 
            // * In order for the dynamic proxy to be created as a navigation property, 
            // * the virtual type must implement ICollection.
            // * source: https://stackoverflow.com/a/10113331/4486196
            // */

            //return Ok(bills);
            return Ok(billService.GetBillsByBuyer(buyerId));
        }

        // ZADATAK 3.3.10
        // GET project/bills/findByCategory/{categoryId}
        [Route("project/bills/findByCategory/{categoryId}")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<BillModel>))]
        public IHttpActionResult GetBillsByCategory(int categoryId)
        {
            //CategoryModel category = db.CategoriesRepository.GetByID(categoryId);
            //if (category == null)
            //{
            //    return NotFound();
            //}

            //List<OfferModel> offers = category.offerModels.ToList();
            //List<BillModel> bills = new List<BillModel>();

            //foreach (var offer in offers)
            //{
            //    bills.AddRange(offer.billModels);
            //}

            //return Ok(bills);
            return Ok(billService.GetBillsByCategory(categoryId));
        }

        // ZADATAK 3.3.11
        // GET project/bills/findByDate/{startDate}/and/{endDate}
        [Route("project/bills/findByDate/{startDate}/and/{endDate}")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<BillModel>))]
        public IHttpActionResult GetBillsInDateRange(DateTime startDate, DateTime endDate)
        {
            //return Ok(db.BillsRepository.Get(
            //    filter: b => b.billCreated >= startDate && b.billCreated <= endDate));
            return Ok(billService.GetBillsByDatePeriod(startDate, endDate));
        }
    }
}
 
 