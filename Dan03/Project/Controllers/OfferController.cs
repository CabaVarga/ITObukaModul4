using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project.Controllers
{
    public class OfferController : ApiController
    {
        private List<OfferModel> GetDB() {
            return new List<OfferModel>
            {
                new OfferModel(10, "", "", new DateTime(2010, 3, 12), new DateTime(2011, 3, 12),
                18, 10.00, "", 12, 10, OfferStatus.WAIT_FOR_APPROVING),
                new OfferModel(11, "", "", new DateTime(2010, 3, 12), new DateTime(2011, 3, 12),
                12.44, 11.00, "", 12, 9, OfferStatus.APPROVED),
                new OfferModel(12, "", "", new DateTime(2010, 3, 12), new DateTime(2011, 3, 12),
                13.24, 12.00, "", 10, 9, OfferStatus.WAIT_FOR_APPROVING),
                new OfferModel(13, "", "", new DateTime(2010, 3, 12), new DateTime(2011, 3, 12),
                19.44, 13.00, "", 10, 10, OfferStatus.DECLINED),
            };
        }
        
        // Zadatak 3.3
        [Route("project/offers")]
        [HttpGet]
        public List<OfferModel> GetOffers()
        {
            return GetDB();
        }

        // Zadatak 3.4
        [Route("project/offers")]
        [HttpPost]
        public OfferModel PostNewOffer([FromBody]OfferModel newOffer)
        {
            List<OfferModel> offers = GetDB();
            offers.Add(newOffer);

            return offers.Find(x => x == newOffer);
        }

        // Zadatak 3.5
        [Route("project/offers/{id}")]
        [HttpPut]
        public OfferModel PutChangeOffer(int id, [FromBody]OfferModel offer)
        {
            List<OfferModel> offers = GetDB();
            OfferModel handle = offers.Find(x => x.Id == id);
            
            if (handle != null)
            {
                handle.OfferName = offer.OfferName;
                handle.OfferDescription = offer.OfferDescription;
                handle.OfferCreated = offer.OfferCreated;
                handle.OfferExpires = offer.OfferExpires;
                handle.RegularPrice = offer.RegularPrice;
                handle.ActionPrice = offer.ActionPrice;
                handle.ImagePath = offer.ImagePath;
                handle.AvailableOffers = offer.AvailableOffers;
                handle.BoughtOffers = offer.BoughtOffers;
            }

            return handle;
        }

        // Zadatak 3.6
        [Route("project/offers/{id}")]
        [HttpDelete]
        public OfferModel DeleteOffer(int id)
        {
            List<OfferModel> offers = GetDB();
            OfferModel offer = offers.Find(x => x.Id == id);
            
            if (offer != null)
            {
                offers.Remove(offer);
            }

            return offer;
        }

        // Zadatak 3.7
        [Route("project/offers/{id}")]
        [HttpGet]
        public OfferModel GetOfferById(int id)
        {
            return GetDB().Find(x => x.Id == id);
        }

        // Zadatak 3.8
        [Route("project/offers/changeOffer/{id}/status/{status}")]
        [HttpPut]
        public OfferModel PutChangeOfferStatus(int id, OfferStatus status)
        {
            List<OfferModel> offers = GetDB();
            OfferModel offer = offers.Find(x => x.Id == id);
            if (offer != null)
            {
                offer.OfferStatus = status;
            }

            return offer;
        }

        // Zadatak 3.9
        [Route("project/offers/findByPrice/{lowerPrice}/and/{upperPrice}")]
        [HttpGet]
        public List<OfferModel> GetOffersByActivePriceRange(double lowerPrice, double upperPrice)
        {
            var offers = GetDB();
            var offersInPriceRange = offers
                .Where(x => x.ActionPrice >= lowerPrice && x.ActionPrice <= upperPrice).ToList();
            return offersInPriceRange;
        }
    }
}
