using Project_3rd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd.Services
{
    public interface IOfferService
    {
        IEnumerable<OfferModel> GetAllOffers();
        OfferModel GetOffer(int id);
        OfferModel UpdateOffer(int id, OfferModel offerModel);
        OfferModel CreateOffer(OfferModel offer);
        OfferModel DeleteOffer(int id);
        OfferModel UpdateOfferStatus(int id, OfferModel.OfferStatus newStatus);
        IEnumerable<OfferModel> GetOffersByActionPriceRange(decimal lowerPrice, decimal upperPrice);
        OfferModel UpdateOffer(OfferModel offer, bool isBillCreated);
        OfferModel UpdateOfferImage(string path, int id);
        // ADDED FOR 4.2.1
        IEnumerable<OfferModel> GetOffersByCategoryAndNotExpired(int categoryId, DateTime expiration);
    }
}