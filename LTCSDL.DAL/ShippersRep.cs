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

    public class ShippersRep : GenericRep<NorthwindContext, Shippers>
    {
        #region -- Overrides --

        
        public override Shippers Read(int id)
        {
            var res = All.FirstOrDefault(p => p.ShipperId == id);
            return res;
        }


        /// <summary>
        /// Remove and not restore
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Number of affect</returns>
        public int Remove(int id)
        {
            var m = base.All.First(i => i.ShipperId == id);
            m = base.Delete(m); //TODO
            return m.ShipperId;
        }

        #endregion

        #region -- Methods --

        //Đề 3 Câu 3
        public Shippers SP_ThemShippers(Shippers sp)
        {
            Shippers x = new Shippers();
            var cnn = (SqlConnection)Context.Database.GetDbConnection();
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                string sql = "INSERT INTO [Shippers] ([CompanyName] ,[Phone])  VALUES('" + sp.CompanyName + "','" + sp.Phone + "');";
                sql = sql + " SELECT * FROM [Shippers] WHERE ShipperID = @@IDENTITY";
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cnn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        x = new Shippers
                        {
                            ShipperId = Int16.Parse(row["ShipperID"].ToString()),
                            CompanyName = row["CompanyName"].ToString(),
                            Phone = row["Phone"].ToString()
                        };
                    }
                }

            }
            catch (Exception e)
            {
                x = null;
            }
            return x;
        }

        //Chap nhat shippers
        public Shippers CapNhatShippers(Shippers ship)
        {
            Shippers x = new Shippers();
            var cnn = (SqlConnection)Context.Database.GetDbConnection();
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                String sql = "UPDATE [Shippers] SET [CompanyName] = '" + ship.CompanyName + "',[Phone] = '" +
                    ship.Phone + "' WHERE ShipperID = " + ship.ShipperId + ";";
                sql += sql + "select * from Shippers where ShipperID = " + ship.ShipperId.ToString();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cnn.CreateCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        x = new Shippers
                        {
                            ShipperId = Int16.Parse(row["ShipperID"].ToString()),
                            CompanyName = row["CompanyName"].ToString(),
                            Phone = row["Phone"].ToString()
                        };
                    }
                }

            }
            catch (Exception e)
            {
                x = null;
            }
            return x;
        }

        // De4 cau 4a Linq
        public object SP_DanhSachShipper_OT_Linq(int month, int year)
        {
            var or = Context.Shippers
                .Join(Context.Orders, a => a.ShipperId, b => b.ShipVia, (a, b) => new
                {
                    a.ShipperId,
                    a.CompanyName,
                    a.Phone,
                    b.OrderDate,
                    b.Freight

                })
                .Where(x => x.OrderDate.Value.Month == month)
                .Where(x => x.OrderDate.Value.Year == year)
                .OrderBy(x => x.ShipperId).ToList();

            var res = or.GroupBy(x => x.ShipperId)
                .Select(x => new
                {
                    ShipperId = x.First().ShipperId,
                    CompanyName = x.First().CompanyName,
                    Phone = x.First().Phone,
                    Tien = x.Sum(x => x.Freight)

                }).ToList();

            return res;


        }





        #endregion
    }
}
