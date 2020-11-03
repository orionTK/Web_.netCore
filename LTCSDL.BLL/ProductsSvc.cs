using Newtonsoft.Json;

using LTCSDL.BLL;
using LTCSDL.Common.Rsp;
using LTCSDL.Common.BLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LTCSDL.BLL
{
    using DAL;
    using DAL.Models;
    using LTCSDL.Common.Req;

    public class ProductsSvc : GenericSvc<ProductsRep, Products>
    {
        #region -- Overrides --

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();

            var m = _rep.Read(id);
            res.Data = m;

            return res;
        }


        #endregion

        #region -- Methods --

        //Đề 5: câu 3
        public Products SP_InsertProducts(ProductsReq req)
        {
            Products x = new Products();
            x.ProductName = req.ProductName;
            x.SupplierId = req.SupplierId;
            x.CategoryId = req.CategoryId;
            x.QuantityPerUnit = req.QuantityPerUnit;
            x.UnitPrice = req.UnitPrice;
            x.UnitsInStock = req.UnitsInStock;
            x.UnitsOnOrder = req.UnitsOnOrder;
            x.ReorderLevel = req.ReorderLevel;
            x.Discontinued = req.Discontinued;
            return _rep.SP_InsertProducts(x);
        }
        public ProductsSvc() { }


        #endregion
    }
}
