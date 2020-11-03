using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.Common.Req
{
    public  class OrdersReq
    {
        //public OrdersReq()
        //{
        //    orderId = 0;
        //}

        ///// <summary>
        ///// Initialize
        ///// </summary>
        ///// <param name="id">ID</param>
        //public OrdersReq(int id)
        //{
        //    orderId = id;
        //}
        ///// </summary>
        ///// <param name="createdBy">Created by</param>
        ///// <returns>Return the result</returns>


        //public int orderId { get; set; }
        public int day { get; set; }
        public string keyword { get; set; }
        public string ten { get; set; }
        public DateTime date { get; set; }
        public DateTime dateFrom { get; set; }
        public DateTime dateTo { get; set; }
        public bool isQuantity { get; set; }
        public int page { get; set; }
        public int size { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public int ShipVia { get; set; }
        public decimal Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }


    }
}
