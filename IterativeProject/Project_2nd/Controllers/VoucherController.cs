using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Project_2nd.Models;
using Project_2nd.Repositories;

namespace Project_2nd.Controllers
{
    public class VoucherController : ApiController
    {
        private UnitOfWork db = new UnitOfWork();

        // GET: api/Voucher
        [Route("project/vouchers")]
        public IEnumerable<VoucherModel> GetvoucherModels()
        {
            return db.VoucherRepository.Get();
        }

        // GET: api/Voucher/5
        [Route("project/vouchers/{id}", Name = "SingleVoucherById")]
        [ResponseType(typeof(VoucherModel))]
        public IHttpActionResult GetVoucherModel(int id)
        {
            VoucherModel voucherModel = db.VoucherRepository.GetByID(id);
            if (voucherModel == null)
            {
                return NotFound();
            }

            return Ok(voucherModel);
        }

        // PUT: api/Voucher/5
        [Route("project/vouchers/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVoucherModel(int id, VoucherModel voucherModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != voucherModel.id)
            {
                return BadRequest();
            }

            db.VoucherRepository.Update(voucherModel);
            db.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Voucher
        [Route("project/vouchers")]
        [ResponseType(typeof(VoucherModel))]
        public IHttpActionResult PostVoucherModel(VoucherModel voucherModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VoucherRepository.Insert(voucherModel);
            db.Save();

            return CreatedAtRoute("SingleVoucherById", new { id = voucherModel.id }, voucherModel);
        }

        // DELETE: api/Voucher/5
        [Route("project/vouchers/{id}")]
        [ResponseType(typeof(VoucherModel))]
        public IHttpActionResult DeleteVoucherModel(int id)
        {
            VoucherModel voucherModel = db.VoucherRepository.GetByID(id);
            if (voucherModel == null)
            {
                return NotFound();
            }

            db.VoucherRepository.Delete(id);
            db.Save();

            return Ok(voucherModel);
        }

        // ZADACI 4.6 i 4.8

        // ZADATAK 4.6
        // PUT project/vouchers/4/connect-offer/3
        [Route("project/vouchers/{voucherId}/connect-offer/{offerId}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutConnectBillAndOffer(int voucherId, int offerId)
        {
            var voucher = db.VoucherRepository.GetByID(voucherId);

            if (voucher == null)
            {
                return NotFound();
            }

            OfferModel offer = db.OfferRepository.GetByID(offerId);

            if (offer == null)
            {
                return NotFound();
            }

            voucher.offerModel = offer;
            db.VoucherRepository.Update(voucher);
            db.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // ZADATAK 4.8
        // PUT project/vouchers/4/connect-user/3
        [Route("project/vouchers/{voucherId}/connect-user/{userId}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutConnectBillAnUser(int voucherId, int userId)
        {
            var voucher = db.VoucherRepository.GetByID(voucherId);

            if (voucher == null)
            {
                return NotFound();
            }

            UserModel user = db.UserRepository.GetByID(userId);

            if (user == null)
            {
                return NotFound();
            }

            if (user.user_role != UserModel.UserRoles.ROLE_CUSTOMER)
            {
                return BadRequest("User is not a customer");
            }

            voucher.userModel = user;
            db.VoucherRepository.Update(voucher);
            db.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}