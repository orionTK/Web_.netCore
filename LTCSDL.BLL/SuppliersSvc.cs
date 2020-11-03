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

    public class SuppliersSvc : GenericSvc<SuppliersRep, Suppliers>
    {
        #region -- Overrides --

       

        #endregion

        #region -- Methods --


        //Đề 4: câu 2a
        public List<object> SP_ThemSupplierOT(string CompanyName, string ContactName, string ContactTitle, string Address, string City,
            string Region, string PostalCode, string Country, string Phone, string Fax, string HomePage)
        {
            return _rep.SP_ThemSupplierOT(CompanyName, ContactName, ContactTitle, Address, City,
             Region, PostalCode, Country, Phone, Fax, HomePage);
        }

        //Đề 4: câu 2b
        public List<object> SP_CapNhapSupplierOT(int SupplierId,string CompanyName, string ContactName, string ContactTitle, string Address, string City,
            string Region, string PostalCode, string Country, string Phone, string Fax, string HomePage)
        {
            return _rep.SP_CapNhapSupplierOT(SupplierId,CompanyName, ContactName, ContactTitle, Address, City,
             Region, PostalCode, Country, Phone, Fax, HomePage);
        }

        //Đề 4: câu 3
        public Suppliers SP_CapNhatSupplier_LinqEF(SuppliersReq req)
        {
            Suppliers x = new Suppliers();
            x.CompanyName = req.CompanyName;
            x.ContactName = req.ContactName;
            x.ContactTitle = req.ContactTitle;
            x.Address = req.Address;
            x.City = req.City;
            x.Region = req.Region;
            x.PostalCode = req.PostalCode;
            x.Country = req.Country;
            x.Phone = req.Phone;
            x.Fax = req.Fax;
            x.HomePage = req.HomePage;
            x.SupplierId = req.SupplierId;
            return _rep.SP_CapNhatSupplier_LinqEF(x);
        }


        ////Đề 3: câu 4a
        //public object SP_DanhSachSPBanChay_Linq(int month, int year, int page, int size, bool isQuanity)
        //{
        //    return _rep.SP_DanhSachSPBanChay_Linq(month, year, page, size, isQuanity);
        //}

        ////Đề 3: câu 4a
        //public object SP_DanhSachDoanhThuTheoQG_Linq(int month, int year)
        //{
        //    return _rep.SP_DanhSachDoanhThuTheoQG_Linq(month, year);
        //}
        public SuppliersSvc() { }


        #endregion
    }
}
