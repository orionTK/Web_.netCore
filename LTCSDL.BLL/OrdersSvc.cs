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

    public class OrdersSvc : GenericSvc<OrdersRep, Orders>
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


        //Đề 3: câu 2a
        public List<object> SP_DanhSachHoaDonNhanVien(string ten, DateTime dateFrom, DateTime dateTo, int page, int size)
        {
            return _rep.SP_DanhSachHoaDonNhanVien(ten , dateFrom, dateTo, page, size);
        }

        //Đề 3: câu 2b
        public List<object> SP_DanhSachSPBanChay(int month, int year, int page, int size, bool isQuanity)
        {
            return _rep.SP_DanhSachSPBanChay(month, year, page, size, isQuanity);
        }


        //Đề 3: câu 4a
        public object SP_DanhSachSPBanChay_Linq(int month, int year, int page, int size, bool isQuanity)
        {
            return _rep.SP_DanhSachSPBanChay_Linq(month, year, page, size, isQuanity);
        }

        //Đề 3: câu 4a
        public object SP_DanhSachDoanhThuTheoQG_Linq(int month, int year)
        {
            return _rep.SP_DanhSachDoanhThuTheoQG_Linq(month, year);
        }

        //Đề 5: câu 2a
        public List<object> SP_DanhSachKhongCoDH_OT(DateTime date, int page, int size)
        {
            return _rep.SP_DanhSachKhongCoDH_OT(date, page, size);
        }

        //Đề 5: câu 2c
        public List<object> SP_DanhSachDoanhThuTimKiem_OT(string keyword, int page, int size)
        {
            return _rep.SP_DanhSachDoanhThuTimKiem_OT(keyword, page, size);
        }

        //Đề 5: câu 4
        public object SP_DanhSachDonHangKhachHang(int day, int month, int year, int page, int size)
        {
            return _rep.SP_DanhSachDonHangKhachHang(day, month, year, page, size);
        }

        //Đề 5: câu 5
        public object SP_SoLuongSPCanGia(DateTime dateFrom, DateTime dateTo)
        {
            return _rep.SP_SoLuongSPCanGia( dateFrom, dateTo);
        }

        //Đề 2: câu 2a
        public List<object> SP_DanhSachDHDe2_OT(DateTime dateFrom, DateTime dateTo)
        {
            return _rep.SP_DanhSachDHDe2_OT(dateFrom, dateTo);
        }

        //Đề 2: câu 2a
        public List<object> SP_ChiTietDonHang_De2_Cau1(int orderId)
        {
            return _rep.SP_ChiTietDonHang_De2_Cau1(orderId);
        }

        //Đề 2: câu 3a
        public object SP_DanhSachDHDe2_OT_Linq(DateTime dateFrom, DateTime dateTo, int page, int size)
        {
            return _rep.SP_DanhSachDHDe2_OT_Linq(dateFrom, dateTo, page, size);
        }


        //Đề 2: câu 3b
        public object SP_ChiTietDonHang_De2_Cau1_Linq(int orderId)
        {
            return _rep.SP_ChiTietDonHang_De2_Cau1_Linq(orderId);
        }
        public OrdersSvc() { }


        #endregion


    }
}
