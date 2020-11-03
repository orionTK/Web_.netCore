using System;
using System.Collections.Generic;

namespace LTCSDL.DAL.Models
{
    public partial class CustomerOrderAmt
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public double? TotalAmtOfOrder { get; set; }
    }
}
