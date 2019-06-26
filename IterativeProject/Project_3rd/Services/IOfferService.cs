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
    }
}