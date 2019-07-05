using Project_3rd_clean.Models;
using Project_3rd_clean.Models.DTOs.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd_clean.Services
{
    public interface IOffersService
    {
        IEnumerable<Offer> GetAllOffers();
        Offer GetOffer(int id);
        Offer UpdateOffer(int id, Offer offerModel);
        Offer CreateOffer(Offer offer);
        Offer DeleteOffer(int id);
        Offer UpdateOfferStatus(int id, Offer.OfferStatus newStatus);
        IEnumerable<Offer> GetOffersByActionPriceRange(decimal lowerPrice, decimal upperPrice);
        Offer UpdateOffer(Offer offer, bool isBillCreated);
        Offer UpdateOfferImage(string path, int id);
        // ADDED FOR 4.2.1
        IEnumerable<Offer> GetOffersByCategoryAndNotExpired(int categoryId, DateTime expiration);

        // PPA
        IEnumerable<PublicOfferDTO> GetAllOffersPublic();
        IEnumerable<PrivateOfferDTO> GetAllOffersPrivate();
        IEnumerable<AdminOfferDTO> GetAllOffersAdmin();
    }
}