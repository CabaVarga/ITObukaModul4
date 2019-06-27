using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Project_3rd.Models;
using Project_3rd.Repositories;

namespace Project_3rd.Controllers
{
    public class VoucherController : ApiController
    {
        private IUnitOfWork db;

        public VoucherController(IUnitOfWork db)
        {
            this.db = db;
        }

        // GET: api/Voucher
        [Route("project/vouchers")]
        public IEnumerable<VoucherModel> GetvoucherModels()
        {
            return db.VouchersRepository.Get();
        }

        // GET: api/Voucher/5
        [Route("project/vouchers/{id}", Name = "SingleVoucherById")]
        [ResponseType(typeof(VoucherModel))]
        public IHttpActionResult GetVoucherModel(int id)
        {
            VoucherModel voucherModel = db.VouchersRepository.GetByID(id);
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

            db.VouchersRepository.Update(voucherModel);
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

            db.VouchersRepository.Insert(voucherModel);
            db.Save();

            return CreatedAtRoute("SingleVoucherById", new { id = voucherModel.id }, voucherModel);
        }

        // DELETE: api/Voucher/5
        [Route("project/vouchers/{id}")]
        [ResponseType(typeof(VoucherModel))]
        public IHttpActionResult DeleteVoucherModel(int id)
        {
            VoucherModel voucherModel = db.VouchersRepository.GetByID(id);
            if (voucherModel == null)
            {
                return NotFound();
            }

            db.VouchersRepository.Delete(id);
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
            var voucher = db.VouchersRepository.GetByID(voucherId);

            if (voucher == null)
            {
                return NotFound();
            }

            OfferModel offer = db.OffersRepository.GetByID(offerId);

            if (offer == null)
            {
                return NotFound();
            }

            voucher.offerModel = offer;
            db.VouchersRepository.Update(voucher);
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
            var voucher = db.VouchersRepository.GetByID(voucherId);

            if (voucher == null)
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

            voucher.userModel = user;
            db.VouchersRepository.Update(voucher);
            db.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}