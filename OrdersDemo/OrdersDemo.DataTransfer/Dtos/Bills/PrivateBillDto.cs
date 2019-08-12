using OrdersDemo.DataTransfer.Dtos.Offers;
using OrdersDemo.DataTransfer.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersDemo.DataTransfer.Dtos.Bills
{
    public class PrivateBillDto : PublicBillDto
    {
        public PrivateUserDto Buyer { get; set; }

        public PublicOfferDto Offer { get; set; }
    }
}
