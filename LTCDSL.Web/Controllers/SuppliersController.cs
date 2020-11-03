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
    using LTCSDL.DAL;

    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        public SuppliersController()
        {
            _svc = new SuppliersSvc();
        }

        //Đề 4 câu 2a
        [HttpPost("SP-Them-Supplier-OT")]
        public IActionResult SP_ThemSupplierOT([FromBody]SuppliersReq req)
        {
            var res = new SingleRsp();
            var hist = _svc.SP_ThemSupplierOT(req.CompanyName, req.ContactName, req.ContactTitle, req.Address, req.City,
             req.Region, req.PostalCode, req.Country, req.Phone, req.Fax, req.HomePage);
            res.Data = hist;
            return Ok(res);
        }

        //Đề 4 câu 2b
        [HttpPost("SP-Cap-Nhap-Supplier-OT")]
        public IActionResult SP_CapNhapSupplierOT([FromBody]SuppliersReq req)
        {
            var res = new SingleRsp();
            var hist = _svc.SP_CapNhapSupplierOT(req.SupplierId,req.CompanyName, req.ContactName, req.ContactTitle, req.Address, req.City,
             req.Region, req.PostalCode, req.Country, req.Phone, req.Fax, req.HomePage);
            res.Data = hist;
            return Ok(res);
        }

        //Đề 4 câu 3
        [HttpPost("SP-Cap-Nhat-Supplier-Linq-EF")]
        public IActionResult SP_CapNhatSupplier_LinqEF([FromBody]SuppliersReq req)
        {
            var res = new SingleRsp();
            var hist = _svc.SP_CapNhatSupplier_LinqEF(req);
            res.Data = hist;
            return Ok(res);
        }

        ////Đề 4 câu 3
        //[HttpPost("SP-Cap-Nhap-Supplier-OT")]
        //public IActionResult SP_CapNhapSupplierOT([FromBody]SuppliersReq req)
        //{
        //    var res = new SingleRsp();
        //    var hist = _svc.SP_CapNhapSupplierOT(req.SupplierId, req.CompanyName, req.ContactName, req.ContactTitle, req.Address, req.City,
        //     req.Region, req.PostalCode, req.Country, req.Phone, req.Fax, req.HomePage);
        //    res.Data = hist;
        //    return Ok(res);
        //}

        ////Đề 3 câu 4a
        //[HttpPost("SP-Dan-hSach-SP-Ban-Chay-Linq")]
        //public IActionResult SP_DanhSachSPBanChay_Linq([FromBody]OrdersReq req)
        //{
        //    var res = new SingleRsp();
        //    var hist = _svc.SP_DanhSachSPBanChay_Linq(req.month, req.year, req.page, req.size, req.isQuantity);
        //    res.Data = hist;
        //    return Ok(res);

        //}
        ////Đề 3 câu 4b
        //[HttpPost("SP-Danh-Sach-Doanh-Thu-Theo-QG-Linq")]
        //public IActionResult SP_DanhSachDoanhThuTheoQG_Linq([FromBody]OrdersReq req)
        //{
        //    var res = new SingleRsp();
        //    var hist = _svc.SP_DanhSachDoanhThuTheoQG_Linq(req.month, req.year);
        //    res.Data = hist;
        //    return Ok(res);
        //}

        private readonly SuppliersSvc _svc;
    }
}