using Project_3rd.Models;
using Project_3rd.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd.Services
{
    public class OfferService : IOfferService
    {
        private IUnitOfWork db;

        public OfferService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<OfferModel> GetAllOffers()
        {
            return db.OffersRepository.Get();
        }

        public OfferModel GetOffer(int id)
        {
            return db.OffersRepository.GetByID(id);
        }

        public OfferModel UpdateOffer(int id, OfferModel offerModel)
        {
            OfferModel offer = db.OffersRepository.GetByID(id);

            if (offer != null)
            {
                offer.offer_name = offerModel.offer_name;
                offer.offer_description = offerModel.offer_description;
                offer.offer_created = offerModel.offer_created;
                offer.offer_expires = offerModel.offer_expires;
                offer.regular_price = offerModel.regular_price;
                offer.action_price = offerModel.action_price;
                offer.image_path = offerModel.image_path;
                offer.available_offers = offerModel.available_offers;
                offer.bought_offers = offerModel.bought_offers;
                offer.categoryModel = offerModel.categoryModel;
                offer.userModel = offerModel.userModel;

                db.OffersRepository.Update(offer);
                db.Save();
            }

            return offer;
        }
    }
}