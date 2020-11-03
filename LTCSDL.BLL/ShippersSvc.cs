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

    public class ShippersSvc : GenericSvc<ShippersRep, Shippers>
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

        //Cap nhat
        public Shippers CapNhatShippers(ShippersReq req)
        {
            Shippers x = new Shippers();
            x.ShipperId = req.ShipperId;
            x.CompanyName = req.CompanyName;
            x.Phone = req.Phone;
            return _rep.CapNhatShippers(x);
        }

        //Them shipper
        public Shippers SP_ThemShippers(ShippersReq req)
        {
            Shippers ship = new Shippers();
            ship.CompanyName = req.CompanyName;
            ship.Phone = req.Phone;
            return _rep.SP_ThemShippers(ship);
        }

        //Đề 4 câu 4
        public object SP_DanhSachShipper_OT_Linq(int month, int year)
        {
            return _rep.SP_DanhSachShipper_OT_Linq(month, year);
        }

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public ShippersSvc() { }


        #endregion
    }
}
