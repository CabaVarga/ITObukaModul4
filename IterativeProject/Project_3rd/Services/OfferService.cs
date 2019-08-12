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
                offer.category = offerModel.category;
                offer.seller = offerModel.seller;

                db.OffersRepository.Update(offer);
                db.Save();
            }

            return offer;
        }

        public OfferModel CreateOffer(OfferModel offer)
        {
            // TODO Extract category & user logic from the controller and put it here....
            // PRO: cleaning up the controller - it would not need to hold any reference to services other than offerService
            // CONTRA: this service would need to either hold a reference to user and category services
            //         or to directly work with db, accessing the user and category repositories...
            //         serious QUESTION!
            // Business logic
            offer.offer_status = OfferModel.OfferStatus.WAIT_FOR_APPROVING;
            offer.offer_created = DateTime.UtcNow;
            offer.offer_expires = offer.offer_created.AddDays(10);

            db.OffersRepository.Insert(offer);
            db.Save();

            return offer;
        }

        public OfferModel DeleteOffer(int id)
        {
            OfferModel offer = db.OffersRepository.GetByID(id);

            if (offer != null)
            {
                db.OffersRepository.Delete(id);
                db.Save();
            }

            return offer;
        }

        public OfferModel UpdateOfferStatus(int id, OfferModel.OfferStatus newStatus)
        {
            OfferModel offer = db.OffersRepository.GetByID(id);

            if (offer != null)
            {
                offer.offer_status = newStatus;
                db.OffersRepository.Update(offer);
                db.Save();
            }

            // ukoliko se ponuda proglasi isteklom potrebno je otkazati sve racune koji sadrze tu ponudu
            if (newStatus == OfferModel.OfferStatus.EXPIRED)
            {
                // racuni koji sadrze ponudu
                var bills = db.BillsRepository.Get(
                    filter: b => b.offerId == id);

                foreach (var bill in bills)
                {
                    bill.paymentCancelled = true;
                    db.BillsRepository.Update(bill);
                }

                db.Save();
            }

            return offer;
        }

        public IEnumerable<OfferModel> GetOffersByActionPriceRange(decimal lowerPrice, decimal upperPrice)
        {
            IEnumerable<OfferModel> offersInPriceRange = db.OffersRepository.Get(
                filter: o => o.action_price >= lowerPrice && o.action_price <= upperPrice);

            return offersInPriceRange;
        }

        public OfferModel UpdateOffer(OfferModel offer, bool isBillCreated)
        {
            if (isBillCreated)
            {
                offer.available_offers--;
                offer.bought_offers++;
            }
            else
            {
                offer.available_offers++;
                offer.bought_offers--;
            }

            db.OffersRepository.Update(offer);
            db.Save();

            return offer;
        }

        public OfferModel UpdateOfferImage(string path, int id)
        {
            OfferModel offer = db.OffersRepository.GetByID(id);

            if (offer != null)
            {
                offer.image_path = path;
                db.OffersRepository.Update(offer);
                db.Save();
            }

            return offer;
        }

        public IEnumerable<OfferModel> GetOffersByCategoryAndNotExpired(int categoryId, DateTime expiration)
        {
            return db.OffersRepository.Get(
                filter: o => o.categoryId == categoryId && o.offer_expires < expiration);
        }
    }
}