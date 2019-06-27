using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Project_3rd.Models;
using Project_3rd.Repositories;
using Project_3rd.Services;

namespace Project_3rd.Controllers
{
    public class VoucherController : ApiController
    {
        private IUnitOfWork db;
        private IVoucherService voucherService;
        private IOfferService offerService;
        private IUserService userService;

        public VoucherController(IUnitOfWork db, IVoucherService voucherService, IOfferService offerService, IUserService userService)
        {
            this.db = db;
            this.voucherService = voucherService;
            this.offerService = offerService;
            this.userService = userService;
        }

        // GET: api/Voucher
        [Route("project/vouchers")]
        public IEnumerable<VoucherModel> GetvoucherModels()
        {
            return voucherService.GetAllVouchers();
        }

        // GET: api/Voucher/5
        [Route("project/vouchers/{id}", Name = "SingleVoucherById")]
        [ResponseType(typeof(VoucherModel))]
        public IHttpActionResult GetVoucherModel(int id)
        {
            VoucherModel voucherModel = voucherService.GetVoucher(id);

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
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (id != voucherModel.id)
            //{
            //    return BadRequest();
            //}

            //voucherService.UpdateVoucher(id, voucherModel);

            //return StatusCode(HttpStatusCode.NoContent);

            if (!ModelState.IsValid || voucherModel.offerId == null || voucherModel.userId == null)
            {
                return BadRequest(ModelState);
            }

            if (id != voucherModel.id)
            {
                return BadRequest();
            }

            OfferModel offer = offerService.GetOffer((int)voucherModel.offerId);
            UserModel buyer = userService.GetUser((int)voucherModel.userId);

            if (offer == null || buyer == null)
            {
                return NotFound();
            }

            if (buyer.user_role != UserModel.UserRoles.ROLE_CUSTOMER)
            {
                return BadRequest("User's role must be ROLE_CUSTOMER");
            }

            voucherModel.offerModel = offer;
            voucherModel.userModel = buyer;
            VoucherModel updatedVoucher = voucherService.UpdateVoucher(id, voucherModel);

            if (updatedVoucher == null)
            {
                return NotFound();
            }

            return Ok(updatedVoucher);
        }

        // POST: api/Voucher
        [Route("project/vouchers")]
        [ResponseType(typeof(VoucherModel))]
        public IHttpActionResult PostVoucherModel(VoucherModel voucher)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //VoucherModel createdVoucher = voucherService.CreateVoucher(voucherModel);

            //return CreatedAtRoute("SingleVoucherById", new { id = createdVoucher.id }, createdVoucher);
            if (!ModelState.IsValid || voucher.offerId == null || voucher.userId == null)
            {
                return BadRequest(ModelState);
            }

            OfferModel offer = offerService.GetOffer((int)voucher.offerId);
            UserModel buyer = userService.GetUser((int)voucher.userId);

            if (offer == null || buyer == null)
            {
                return NotFound();
            }

            if (buyer.user_role != UserModel.UserRoles.ROLE_CUSTOMER)
            {
                return BadRequest("User's role must be ROLE_CUSTOMER");
            }

            voucher.offerModel = offer;
            voucher.userModel = buyer;
            VoucherModel createdVoucher = voucherService.CreateVoucher(voucher);

            return CreatedAtRoute("PostVoucher", new { id = createdVoucher.id }, createdVoucher);
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

        #region Connect Voucher with user and offer -- I've made a mistake again, there's no need for additional methods!!!
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
        #endregion
    }
}