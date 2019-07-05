using Project_3rd_clean.Models;
using Project_3rd_clean.Models.DTOs.Category;
using Project_3rd_clean.Models.DTOs.Offer;
using Project_3rd_clean.Models.DTOs.User;
using Project_3rd_clean.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_3rd_clean.Services
{
    public class OffersService : IOffersService
    {
        private IUnitOfWork db;

        public OffersService(IUnitOfWork db)
        {
            this.db = db;
        }

        public IEnumerable<Offer> GetAllOffers()
        {
            return db.OffersRepository.Get();
        }

        public Offer GetOffer(int id)
        {
            return db.OffersRepository.GetByID(id);
        }

        public Offer UpdateOffer(int id, Offer offerModel)
        {
            Offer offer = db.OffersRepository.GetByID(id);

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

        public Offer CreateOffer(Offer offer)
        {
            // TODO Extract category & user logic from the controller and put it here....
            // PRO: cleaning up the controller - it would not need to hold any reference to services other than offerService
            // CONTRA: this service would need to either hold a reference to user and category services
            //         or to directly work with db, accessing the user and category repositories...
            //         serious QUESTION!
            // Business logic
            offer.offer_status = Offer.OfferStatus.WAIT_FOR_APPROVING;
            offer.offer_created = DateTime.UtcNow;
            offer.offer_expires = offer.offer_created.AddDays(10);

            db.OffersRepository.Insert(offer);
            db.Save();

            return offer;
        }

        public Offer DeleteOffer(int id)
        {
            Offer offer = db.OffersRepository.GetByID(id);

            if (offer != null)
            {
                db.OffersRepository.Delete(id);
                db.Save();
            }

            return offer;
        }

        public Offer UpdateOfferStatus(int id, Offer.OfferStatus newStatus)
        {
            Offer offer = db.OffersRepository.GetByID(id);

            if (offer != null)
            {
                offer.offer_status = newStatus;
                db.OffersRepository.Update(offer);
                db.Save();
            }

            return offer;
        }

        public IEnumerable<Offer> GetOffersByActionPriceRange(decimal lowerPrice, decimal upperPrice)
        {
            IEnumerable<Offer> offersInPriceRange = db.OffersRepository.Get(
                filter: o => o.action_price >= lowerPrice && o.action_price <= upperPrice);

            return offersInPriceRange;
        }

        public Offer UpdateOffer(Offer offer, bool isBillCreated)
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

        public Offer UpdateOfferImage(string path, int id)
        {
            Offer offer = db.OffersRepository.GetByID(id);

            if (offer != null)
            {
                offer.image_path = path;
                db.OffersRepository.Update(offer);
                db.Save();
            }

            return offer;
        }

        public IEnumerable<Offer> GetOffersByCategoryAndNotExpired(int categoryId, DateTime expiration)
        {
            return db.OffersRepository.Get(
                filter: o => o.categoryId == categoryId && o.offer_expires < expiration);
        }

        public IEnumerable<PublicOfferDTO> GetAllOffersPublic()
        {
            return db.OffersRepository.Get()
                .Select(o =>
                {
                    return new PublicOfferDTO()
                    {
                        Id = o.id,
                        Name = o.offer_name,
                        Description = o.offer_description,
                        Created = o.offer_created,
                        ExpirationDate = o.offer_expires,
                        RegularPrice = o.regular_price,
                        ActionPrice = o.action_price,
                        AvailableOffers = o.available_offers,
                        BoughtOffers = o.bought_offers,
                        ImagePath = o.image_path,
                        Category = new PublicCategoryDTO()
                        {
                            Description = o.category.category_description,
                            Id = o.category.id,
                            Name = o.offer_name
                        },
                        Seller = new PublicUserDTO()
                        {
                            Id = o.seller.id,
                            UserName = o.seller.username
                        },
                        Status = o.offer_status
                    };
                });
        }

        public IEnumerable<PrivateOfferDTO> GetAllOffersPrivate()
        {
            return db.OffersRepository.Get()
                .Select(o =>
                {
                    return new PrivateOfferDTO()
                    {
                        Id = o.id,
                        Name = o.offer_name,
                        Description = o.offer_description,
                        Created = o.offer_created,
                        ExpirationDate = o.offer_expires,
                        RegularPrice = o.regular_price,
                        ActionPrice = o.action_price,
                        AvailableOffers = o.available_offers,
                        BoughtOffers = o.bought_offers,
                        ImagePath = o.image_path,
                        Category = new PrivateCategoryDTO()
                        {
                            Description = o.category.category_description,
                            Id = o.category.id,
                            Name = o.offer_name
                        },
                        Seller = new PrivateUserDTO()
                        {
                            Id = o.seller.id,
                            UserName = o.seller.username,
                            FirstName = o.seller.first_name,
                            LastName = o.seller.last_name
                        },
                        Status = o.offer_status
                    };
                });
        }

        public IEnumerable<AdminOfferDTO> GetAllOffersAdmin()
        {
            return db.OffersRepository.Get()
                .Select(o =>
                {
                    return new AdminOfferDTO()
                    {
                        Id = o.id,
                        Name = o.offer_name,
                        Description = o.offer_description,
                        Created = o.offer_created,
                        ExpirationDate = o.offer_expires,
                        RegularPrice = o.regular_price,
                        ActionPrice = o.action_price,
                        AvailableOffers = o.available_offers,
                        BoughtOffers = o.bought_offers,
                        ImagePath = o.image_path,
                        Category = new AdminCategoryDTO()
                        {
                            Description = o.category.category_description,
                            Id = o.category.id,
                            Name = o.offer_name
                        },
                        Seller = new AdminUserDTO()
                        {
                            Id = o.seller.id,
                            UserName = o.seller.username,
                            LastName = o.seller.last_name,
                            FirstName = o.seller.first_name
                        },
                        Status = o.offer_status
                    };
                });
        }
    }
}