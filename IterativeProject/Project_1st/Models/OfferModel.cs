using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_1st.Models
{
    public enum OfferStatus { WAIT_FOR_APPROVING, APPROVED, DECLINED, EXPIRED };
    public class OfferModel
    {
        public int Id { get; set; }
        public string OfferName { get; set; }
        public string OfferDescription { get; set; }
        public DateTime OfferCreated { get; set; }
        public DateTime OfferExpires { get; set; }
        public double RegularPrice { get; set; }
        public double ActionPrice { get; set; }
        public string ImagePath { get; set; }
        public int AvailableOffers { get; set; }
        public int BoughtOffers { get; set; }
        public OfferStatus OfferStatus { get; set; }

        public OfferModel(int id, string offername, string offerdescription, 
            DateTime offercreated, DateTime offerexpires, double regularprice, double actionprice,
            string imagepath, int availableoffers, int boughtoffers, OfferStatus offerstatus)
        {
            Id = id;
            OfferName = offername;
            OfferDescription = offerdescription;
            OfferCreated = offercreated;
            OfferExpires = offerexpires;
            RegularPrice = regularprice;
            ActionPrice = actionprice;
            ImagePath = imagepath;
            AvailableOffers = availableoffers;
            BoughtOffers = boughtoffers;
            OfferStatus = offerstatus;
        }
    }
}