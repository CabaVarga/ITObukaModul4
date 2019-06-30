using Project_3rd_clean.Models.DTOs.Category;
using Project_3rd_clean.Models.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Project_3rd_clean.Models.Offer;

namespace Project_3rd_clean.Models.DTOs.Offer
{
    public class PublicOfferDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime ExpirationDate { get; set; }

        public decimal RegularPrice { get; set; }

        public decimal ActionPrice { get; set; }

        public string ImagePath { get; set; }

        public int AvailableOffers { get; set; }

        public int BoughtOffers { get; set; }

        public OfferStatus Status { get; set; }

        public PublicCategoryDTO Category { get; set; }

        public PublicUserDTO Seller { get; set; }
    }
}