using OrdersDemo.DataTransfer.Dtos.Categories;
using OrdersDemo.DataTransfer.Dtos.Users;
using OrdersDemo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersDemo.DataTransfer.Dtos.Offers
{
    public class PublicOfferDto
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

        public EOfferStatus Status { get; set; }

        public PublicCategoryDto Category { get; set; }

        public PublicUserDto Seller { get; set; }
    }
}
