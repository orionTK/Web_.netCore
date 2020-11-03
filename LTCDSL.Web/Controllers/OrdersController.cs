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
    public class OrdersController : ControllerBase
    {
        public OrdersController()
        {
            _svc = new OrdersSvc();
        }

        [HttpPost("get-by-id")]
        public IActionResult getOrderById([FromBody]SimpleReq req)
        {
            var res = new SingleRsp();
            res = _svc.Read(req.Id);
            return Ok(res);
        }


        //Đề 3 câu 2a
        [HttpPost("SP-Danh-Sach-Hoa-Don-Nhan-Vien")]
        public IActionResult SP_DanhSachHoaDonNhanVien([FromBody]OrdersReq req)
        {
            var res = new SingleRsp();
            var hist = _svc.SP_DanhSachHoaDonNhanVien(req.ten, req.dateFrom, req.dateTo, req.page, req.size);
            res.Data = hist;
            return Ok(res);
        }

        //Đề 3 câu 2b
        [HttpPost("SP-Danh-Sach-SP-Ban-Chay")]
        public IActionResult SP_DanhSachSPBanChay([FromBody]OrdersReq req)
        {
            var res = new SingleRsp();
            var hist = _svc.SP_DanhSachSPBanChay(req.month, req.year, req.page, req.size, req.isQuantity);
            res.Data = hist;
            return Ok(res);
        }

        //Đề 3 câu 4a
        [HttpPost("SP-Dan-hSach-SP-Ban-Chay-Linq")]
        public IActionResult SP_DanhSachSPBanChay_Linq([FromBody]OrdersReq req)
        {
            var res = new SingleRsp();
            var hist = _svc.SP_DanhSachSPBanChay_Linq(req.month, req.year, req.page, req.size, req.isQuantity);
            res.Data = hist;
            return Ok(res);

        }
        //Đề 3 câu 4b
        [HttpPost("SP-Danh-Sach-Doanh-Thu-Theo-QG-Linq")]
        public IActionResult SP_DanhSachDoanhThuTheoQG_Linq([FromBody]OrdersReq req)
        {
            var res = new SingleRsp();
            var hist = _svc.SP_DanhSachDoanhThuTheoQG_Linq(req.month, req.year);
            res.Data = hist;
            return Ok(res);
        }

        //Đề 5 câu 2a
        [HttpPost("SP-Danh-Sach-Khong-Co-DH-OT")]
        public IActionResult SP_DanhSachKhongCoDH_OT([FromBody]OrdersReq req)
        {
            var res = new SingleRsp();
            var hist = _svc.SP_DanhSachKhongCoDH_OT(req.date, req.page, req.size);
            res.Data = hist;
            return Ok(res);
        }

        //Đề 5 câu 2a
        [HttpPost("SP-Danh-Sach-Doanh-Thu-Tim-Kiem-OT")]
        public IActionResult SP_DanhSachDoanhThuTimKiem_OT([FromBody]OrdersReq req)
        {
            var res = new SingleRsp();
            var hist = _svc.SP_DanhSachDoanhThuTimKiem_OT(req.keyword, req.page, req.size);
            res.Data = hist;
            return Ok(res);
        }

        //Đề 5 câu 2b
        [HttpPost("SP-Danh-Sach-Don-Hang-Khach-Hang")]
        public IActionResult SP_DanhSachDonHangKhachHang([FromBody]OrdersReq req)
        {
            var res = new SingleRsp();
            var hist = _svc.SP_DanhSachDonHangKhachHang(req.day, req.month, req.year, req.page, req.size);
            res.Data = hist;
            return Ok(res);
        }

        //Đề 3 câu 4b
        [HttpPost("SP-So-Luong-SP-Can-Gia")]
        public IActionResult SP_SoLuongSPCanGia([FromBody]OrdersReq req)
        {
            var res = new SingleRsp();
            var hist = _svc.SP_SoLuongSPCanGia(req.dateFrom, req.dateTo);
            res.Data = hist;
            return Ok(res);
        }


        //Đề 2 câu 2a
        [HttpPost("SP-Danh-Sach-DH-De-OT")]
        public IActionResult SP_DanhSachDHDe2_OT([FromBody]OrdersReq req)
        {
            var res = new SingleRsp();
            var hist = _svc.SP_DanhSachDHDe2_OT(req.dateFrom, req.dateTo);
            res.Data = hist;
            return Ok(res);
        }

        //Đề 2 câu 2b
        [HttpPost("SP-Chi-Tiet-Don-Hang-De-Cau")]
        public IActionResult SP_ChiTietDonHang_De2_Cau1([FromBody]OrdersReq req)
        {
            var res = new SingleRsp();
            var hist = _svc.SP_ChiTietDonHang_De2_Cau1(req.OrderId);
            res.Data = hist;
            return Ok(res);
        }

        //Đề 2 câu 3a
        [HttpPost("SP-Danh-Sach-DH-De-OT-Linq")]
        public IActionResult SP_DanhSachDHDe2_OT_Linq([FromBody]OrdersReq req)
        {
            var res = new SingleRsp();
            var hist = _svc.SP_DanhSachDHDe2_OT_Linq(req.dateFrom, req.dateTo, req.page, req.size);
            res.Data = hist;
            return Ok(res);
        }

        //Đề 2 câu 3b
        [HttpPost("SP-Chi-Tiet-Don-Hang-De2-Cau1-Linq")]
        public IActionResult SP_ChiTietDonHang_De2_Cau1_Linq([FromBody]OrdersReq req)
        {
            var res = new SingleRsp();
            var hist = _svc.SP_ChiTietDonHang_De2_Cau1_Linq(req.OrderId);
            res.Data = hist;
            return Ok(res);
        }

        private readonly OrdersSvc _svc;
    }
}