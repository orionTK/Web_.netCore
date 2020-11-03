using LTCSDL.Common.DAL;
using System.Linq;

namespace LTCSDL.DAL
{
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class OrdersRep : GenericRep<NorthwindContext, Orders>
    {
        #region -- Overrides --

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override Orders Read(int id)
        {
            var res = All.FirstOrDefault(p => p.OrderId == id);
            return res;
        }


        /// <summary>
        /// Remove and not restore
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Number of affect</returns>
        public int Remove(int id)
        {
            var m = base.All.First(i => i.OrderId == id);
            m = base.Delete(m); //TODO
            return m.OrderId;
        }

        #endregion

        #region -- Methods --

        //Đề 3 Câu 2a
        public List<object> SP_DanhSachHoaDonNhanVien(string ten, DateTime dateFrom, DateTime dateTo, int page, int size)
        {
            List<object> res = new List<object>();
            var cnn = (SqlConnection)Context.Database.GetDbConnection();
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cnn.CreateCommand();
                cmd.CommandText = "SP_DanhSachHoaDonNhanVien";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ten", ten);
                cmd.Parameters.AddWithValue("@dateFrom", dateFrom);
                cmd.Parameters.AddWithValue("@dateTo", dateTo);
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@size", size);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            STT = row["STT"],
                            FirstName = row["FirstName"],
                            LastName = row["LastName"],
                            OrderID = row["OrderID"],
                            CustomerID = row["CustomerID"],
                            EmployeeID = row["EmployeeID"],
                            OrderDate = row["OrderDate"],
                            RequiredDate = row["RequiredDate"],
                            ShippedDate = row["ShippedDate"],
                            ShipVia = row["ShipVia"],
                            Freight = row["Freight"],
                            ShipName = row["ShipName"],
                            ShipAddress = row["ShipAddress"],
                            ShipCity = row["ShipCity"],
                            ShipRegion = row["ShipRegion"],
                            ShipPostalCode = row["ShipPostalCode"],
                            ShipCountry = row["ShipCountry"]
                        };
                        res.Add(x);
                    }
                }

            }
            catch (Exception e)
            {
                res = null;
            }
            return res;
        }

        //Đề 3 Câu 2b
         public List<object> SP_DanhSachSPBanChay(int month, int year, int page, int size, bool isQuantity)
            {
                List<object> res = new List<object>();
                var cnn = (SqlConnection)Context.Database.GetDbConnection();
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataSet ds = new DataSet();
                    var cmd = cnn.CreateCommand();
                    cmd.CommandText = "SP_DanhSachSPBanChay";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@month", month);
                    cmd.Parameters.AddWithValue("@year", year);
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@size", size);
                    cmd.Parameters.AddWithValue("@isQuantity", isQuantity);
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            var x = new
                            {
                                STT = row["STT"],
                                ProductId = row["ProductID"],
                                ProductName = row["ProductName"],
                                SoLuong = row["SoLuong"],
                                DoanhThu = row["DoanhThu"]
                            };
                            res.Add(x);
                        }
                    }

                }
                catch (Exception e)
                {
                    res = null;
                }
                return res;
            }

        

        // De3 cau 4a Linq
        public object SP_DanhSachSPBanChay_Linq(int month, int year, int page, int size, bool isQuanity)
        {
            var pro = Context.Products
                .Join(Context.OrderDetails, a => a.ProductId, b => b.ProductId, (a, b) => new
                {
                    a.ProductId,
                    a.ProductName,
                    a.SupplierId,
                    a.CategoryId,
                    a.QuantityPerUnit,
                    a.UnitPrice,
                    a.UnitsInStock,
                    a.ReorderLevel,
                    a.UnitsOnOrder,
                    a.Discontinued,
                    b.OrderId,
                    b.Quantity,
                    
                    DoanhThu = (decimal)b.Quantity * (decimal)b.UnitPrice * ( 1 - (decimal)b.Discount)

                })
                .Join(Context.Orders, a => a.OrderId, b => b.OrderId, (a, b) => new
                {
                    a.ProductId,
                    a.ProductName,
                    a.SupplierId,
                    a.CategoryId,
                    a.QuantityPerUnit,
                    a.UnitPrice,
                    a.UnitsInStock,
                    a.ReorderLevel,
                    a.Discontinued,
                    a.UnitsOnOrder,
                    a.Quantity,
                    b.OrderDate,
                    a.DoanhThu
                })
                .Where(x => x.OrderDate.Value.Month == month)
                .Where(x => x.OrderDate.Value.Year == year).ToList();

            var offset = 0;
            var total = 0;
            int totalPage = 0;
           
            if (isQuanity)
            {
                var res = pro.GroupBy(x => x.ProductId)
                .Select(x => new
                {
                    ProductId = x.First().ProductId,
                    ProductName = x.First().ProductName,
                    SupplierId = x.First().SupplierId,
                    CategoryId = x.First().CategoryId,
                    QuantityPerUnit = x.First().QuantityPerUnit,
                    UnitPrice = x.First().UnitPrice,
                    UnitsInStock = x.First().UnitsInStock,
                    UnitsOnOrder = x.First().UnitsOnOrder,
                    ReorderLevel = x.First().ReorderLevel,
                    OrderDate = x.First().OrderDate,
                    SoLuongBan = x.Sum(x => x.Quantity)
                }).ToList();

                offset = (page - 1) * size;
                total = res.Count();
                totalPage = (total % size) == 0 ? (int)(total / size) : (int)((total / size) + 1);
                var data = res.OrderByDescending(x => x.SoLuongBan).Skip(offset).Take(size).ToList();
                return new
                {
                    Data = data,
                    TotalRecords = total,
                    Page = page,
                    Size = size,
                    TotalPages = totalPage

                };
            }
            else
            {
                var res = pro.GroupBy(x => x.ProductId)
                .Select(x => new
                {
                    ProductId = x.First().ProductId,
                    ProductName = x.First().ProductName,
                    SupplierId = x.First().SupplierId,
                    CategoryId = x.First().CategoryId,
                    QuantityPerUnit = x.First().QuantityPerUnit,
                    UnitPrice = x.First().UnitPrice,
                    UnitsInStock = x.First().UnitsInStock,
                    UnitsOnOrder = x.First().UnitsOnOrder,
                    ReorderLevel = x.First().ReorderLevel,
                    OrderDate = x.First().OrderDate,
                    DoanhThu = x.Sum(x => x.DoanhThu)


                }).ToList();

                offset = (page - 1) * size;
                total = res.Count();
                totalPage = (total % size) == 0 ? (int)(total / size) : (int)((total / size) + 1);
                var data = res.OrderByDescending(x => x.DoanhThu).Skip(offset).Take(size).ToList();
                return new
                {
                    Data = data,
                    TotalRecords = total,
                    Page = page,
                    Size = size,
                    TotalPages = totalPage

                };
            }

        
        }


        // De3 cau 4B Linq
        public object SP_DanhSachDoanhThuTheoQG_Linq(int month, int year)
        {
            var or = Context.Orders
                .Join(Context.OrderDetails, a => a.OrderId, b => b.OrderId, (a, b) => new
                {
                    a.ShipCountry,
                    a.OrderDate,
                    DoanhThu = (decimal)b.Quantity * (decimal)b.UnitPrice * (1 - (decimal)b.Discount)

                })
                .Where(x => x.OrderDate.Value.Month == month)
                .Where(x => x.OrderDate.Value.Year == year)
                .OrderBy(x => x.ShipCountry).ToList();

            var res = or.GroupBy(x => x.ShipCountry)
                .Select(x => new
                {
                    ShipCountry = x.First().ShipCountry,
                    DoanhThu = x.Sum(x => x.DoanhThu)

                }).ToList();

            return res;


        }


        //Đề 5 câu 2a
        public List<object> SP_DanhSachKhongCoDH_OT(DateTime date, int page, int size)
        {
            List<object> res = new List<object>();
            var cnn = (SqlConnection)Context.Database.GetDbConnection();
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cnn.CreateCommand();
                cmd.CommandText = "SP_DanhSachKhongCoDH_OT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@size", size);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            STT = row["STT"],
                            ProductID = row["ProductID"],
                            ProductName = row["ProductName"]
                        };
                        res.Add(x);
                    }
                }

            }
            catch (Exception e)
            {
                res = null;
            }
            return res;
        }

        //Đề 5 câu 2c
        public List<object> SP_DanhSachDoanhThuTimKiem_OT(string keyword, int page, int size)
        {
            List<object> res = new List<object>();
            var cnn = (SqlConnection)Context.Database.GetDbConnection();
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cnn.CreateCommand();
                cmd.CommandText = "SP_DanhSachDoanhThuTimKiem_OT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@keyword", keyword);
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@size", size);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            STT = row["STT"],
                            OrderID = row["OrderID"],
                            OrderDate = row["OrderDate"],
                            FirstName = row["FirstName"],
                            LastName = row["LastName"],
                            CompanyName = row["CompanyName"]
                        };
                        res.Add(x);
                    }
                }

            }
            catch (Exception e)
            {
                res = null;
            }
            return res;
        }

        // De5 cau 4 Linq
        public object SP_DanhSachDonHangKhachHang(int day, int month, int year, int page, int size)
        {
            var or = Context.Orders
                .Join(Context.Customers, a => a.CustomerId, b => b.CustomerId, (a, b) => new
                {
                    a.ShipCountry,
                    a.OrderDate,
                    a.OrderId,
                    b.CompanyName

                })
                .Where(x => x.OrderDate.Value.Month == month)
                .Where(x => x.OrderDate.Value.Year == year)
                .Where(x => x.OrderDate.Value.Day == day)
                .OrderBy(x => x.OrderId).ToList();

            var res = or.GroupBy(x => x.OrderId)
                .Select(x => new
                {
                    OrderId = x.First().OrderId,
                    OrderDate = x.First().OrderDate,
                    CompanyName = x.First().CompanyName,
                    ShipCountry = x.First().ShipCountry

                }).ToList();

            var offset = (page - 1) * size;
            var total = res.Count();
            int totalPage = (total % size) == 0 ? (int)(total / size) : (int)((total / size) + 1);
            var data = res.OrderBy(x => x.OrderId).Skip(offset).Take(size).ToList();


            return new
            {
                Data = data,
                TotalRecords = total,
                Page = page,
                Size = size,
                TotalPages = totalPage

            };
            return res;


        }

        // De5 cau 5 Linq
        public object SP_SoLuongSPCanGia(DateTime dateFrom, DateTime dateTo)
        {
            var or = Context.Orders
                .Join(Context.OrderDetails, a => a.OrderId, b => b.OrderId, (a, b) => new
                {
                    a.OrderDate,
                    a.OrderId,
                    b.Quantity

                })
                .Where(x => x.OrderDate >= dateFrom)
                .Where(x => x.OrderDate <= dateTo)
                .OrderBy(x => x.OrderDate).ToList();

            var res = or.GroupBy(x => x.OrderDate)
                .Select(x => new
                {
                    OrderDate = x.First().OrderDate,
                    SoLuong = x.Sum(x => x.Quantity)

                }).ToList();

           
            return res;


        }

        //Đề 2 câu 1a
        public List<object> SP_DanhSachDHDe2_OT(DateTime dateFrom, DateTime dateTo)
        {
            List<object> res = new List<object>();
            var cnn = (SqlConnection)Context.Database.GetDbConnection();
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cnn.CreateCommand();
                cmd.CommandText = "SP_DanhSachDHDe2_OT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@dateFrom", dateFrom);
                cmd.Parameters.AddWithValue("@dateTo", dateTo);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {

                            OrderId = row["OrderID"],
                            CustomerId = row["CustomerID"],
                            EmployeeId = row["EmployeeID"],
                            OrderDate = row["OrderDate"],
                            RequiredDate = row["RequiredDate"],
                            ShippedDate = row["ShippedDate"],
                            ShipVia = row["ShipVia"],
                            Freight = row["Freight"],
                            ShipName = row["ShipName"],
                            ShipAddress = row["ShipAddress"],
                            ShipCity = row["ShipCity"],
                            ShipRegion = row["ShipRegion"],
                            ShipPostalCode = row["ShipPostalCode"],
                            ShipCountry = row["ShipCountry"]
                        };
                        res.Add(x);
                    }
                }

            }
            catch (Exception e)
            {
                res = null;
            }
            return res;
        }

        //Đề 2 câu 2a
        public List<object> SP_ChiTietDonHang_De2_Cau1(int orderId)
        {
            List<object> res = new List<object>();
            var cnn = (SqlConnection)Context.Database.GetDbConnection();
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cnn.CreateCommand();
                cmd.CommandText = "SP_ChiTietDonHang_De2_Cau1";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@orderId", orderId);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {

                            OrderId = row["OrderID"],
                            ProductId = row["ProductID"],
                            UnitPrice = row["UnitPrice"],
                            Quantity = row["Quantity"],
                            Discount = row["Discount"]
                        };
                        res.Add(x);
                    }
                }

            }
            catch (Exception e)
            {
                res = null;
            }
            return res;
        }

        // De2 cau 3a Linq
        public object SP_DanhSachDHDe2_OT_Linq(DateTime dateFrom, DateTime dateTo, int page, int size)
        {
            //chỉ from từ 1 bảng

            var res = All.Where(x => x.OrderDate >= dateFrom && x.OrderDate <= dateTo);

            var offset = (page - 1) * size;
            var total = res.Count();
            int totalPage = (total % size) == 0 ? (int)(total / size) : (int)((total / size) + 1);
            var data = res.OrderBy(x => x.OrderId).Skip(offset).Take(size).ToList();


            return new
            {
                Data = data,
                TotalRecords = total,
                Page = page,
                Size = size,
                TotalPages = totalPage

            };

        }

        // De2 cau 3b Linq
        public object SP_ChiTietDonHang_De2_Cau1_Linq(int orderId)
        {
            var or = Context.OrderDetails
                .Join(Context.Products, a => a.ProductId, b => b.ProductId, (a, b) => new
                {
                    a.OrderId,
                    b.ProductId,
                    b.ProductName,
                    a.Quantity,
                    a.UnitPrice,
                   a.Discount,
                    DH = a.Quantity * a.UnitPrice * (decimal) (1- a.Discount)

                })
                 .Where(x => x.OrderId == orderId)
                 .OrderBy(x => x.OrderId).ToList();
            var res = or.GroupBy(x => x.ProductId)
               .Select(x => new
               {
                   ProductId = x.First().ProductId,
                   ProductName = x.First().ProductName,
                   UnitPrice = x.First().UnitPrice,
                   Quantity = x.First().Quantity,
                   Discount = x.First().Discount,
                   Total = x.Sum(x => x.DH)
               }).ToList();
            return res;


        }
#endregion



    }
}
