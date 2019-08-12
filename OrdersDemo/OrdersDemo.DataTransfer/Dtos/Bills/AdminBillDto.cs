using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersDemo.DataTransfer.Dtos.Bills
{
    public class AdminBillDto : PrivateBillDto
    {
        public bool PaymentMade { get; set; }
        public bool PaymentCancelled { get; set; }
    }
}
