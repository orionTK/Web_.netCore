using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LTCSDL.Web.Controllers
{
    using BLL;
    using DAL.Models;
    using Common.Req;
    using System.Collections.Generic;
    //using BLL.Req;
    using Common.Rsp;

    [Route("api/[controller]")]
    [ApiController]
    public class ShippersController : ControllerBase
    {
        public ShippersController()
        {
            _svc = new ShippersSvc();
        }

       

        //Đề 3 câu 3
        [HttpPost("SP-Them-Shippers")]
        public IActionResult SP_ThemShippers([FromBody] ShippersReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.SP_ThemShippers(req);
            return Ok(res);
        }

        //Đề 4 câu 4
        [HttpPost("SP-Danh-Sach-Shipper-OT-Linq")]
        public IActionResult SP_DanhSachShipper_OT_Linq([FromBody] ShippersReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.SP_DanhSachShipper_OT_Linq(req.month, req.year);
            return Ok(res);
        }

        //Cap nhat
        [HttpPost("Cap-Nhat-Shippers")]
        public IActionResult CapNhatShippers([FromBody]ShippersReq req)
        {
            var res = new SingleRsp();
            var hist = _svc.CapNhatShippers(req);
            res.Data = hist;
            return Ok(res);
        }
        private readonly ShippersSvc _svc;
    }
}