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

    public class SuppliersRep : GenericRep<NorthwindContext, Suppliers>
    {
        #region -- Overrides --

        

        #endregion

        #region -- Methods --

        //Đề 4 câu 2a
        public List<object> SP_ThemSupplierOT(string CompanyName, string ContactName, string ContactTitle,
            string Address, string City,
            string Region, string PostalCode, string Country, string Phone, string Fax, string HomePage)
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
                cmd.CommandText = "SP_ThemSupplierOT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyName", CompanyName);
                cmd.Parameters.AddWithValue("@ContactName", ContactName);
                cmd.Parameters.AddWithValue("@ContactTitle", ContactTitle);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@City", City);
                cmd.Parameters.AddWithValue("@Region", Region);
                cmd.Parameters.AddWithValue("@PostalCode", PostalCode);
                cmd.Parameters.AddWithValue("@Country", Country);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@Fax", Fax);
                cmd.Parameters.AddWithValue("@HomePage", HomePage);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            SupplierId = row["SupplierID"],
                            CompanyName = row["CompanyName"],
                            ContactName = row["ContactName"],
                            ContactTitle = row["ContactTitle"],
                            Address = row["Address"],
                            City = row["City"],
                            Region = row["Region"],
                            PostalCode = row["PostalCode"],
                            Country = row["Country"],
                            Phone = row["Phone"],
                            Fax = row["Fax"],
                            HomePage = row["HomePage"]
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

        //Đề 4 câu 2B
        public List<object> SP_CapNhapSupplierOT(int SupplierID, string CompanyName, string ContactName, string ContactTitle, string Address, string City,
            string Region, string PostalCode, string Country, string Phone, string Fax, string HomePage)
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
                cmd.CommandText = "SP_CapNhapSupplierOT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                cmd.Parameters.AddWithValue("@CompanyName", CompanyName);
                cmd.Parameters.AddWithValue("@ContactName", ContactName);
                cmd.Parameters.AddWithValue("@ContactTitle", ContactTitle);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@City", City);
                cmd.Parameters.AddWithValue("@Region", Region);
                cmd.Parameters.AddWithValue("@PostalCode", PostalCode);
                cmd.Parameters.AddWithValue("@Country", Country);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@Fax", Fax);
                cmd.Parameters.AddWithValue("@HomePage", HomePage);
                da.SelectCommand = cmd;
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            SupplierId = row["SupplierID"],
                            CompanyName = row["CompanyName"],
                            ContactName = row["ContactName"],
                            ContactTitle = row["ContactTitle"],
                            Address = row["Address"],
                            City = row["City"],
                            Region = row["Region"],
                            PostalCode = row["PostalCode"],
                            Country = row["Country"],
                            Phone = row["Phone"],
                            Fax = row["Fax"],
                            HomePage = row["HomePage"]
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

        //Đề 4 Câu 3
        public Suppliers SP_CapNhatSupplier_LinqEF(Suppliers sp)
        {
            Suppliers x = new Suppliers();
            var cnn = (SqlConnection)Context.Database.GetDbConnection();
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                string sql = "UPDATE [Suppliers] SET [CompanyName] = '" +  sp.CompanyName + "', [ContactName] = '" +sp.ContactName
                    + "', [ContactTitle] = '" + sp.ContactTitle + "', [Address] = '" + sp.Address + "', [City] = '" + sp.City + "', [Region] = '" + sp.Region
                    + "', [PostalCode] = '" + sp.PostalCode + "', [Country] = '" + sp.Country + "', [Phone] = '" + sp.Phone
                    + "', [Fax] = '" + sp.Fax + "', [HomePage] = '" + sp.HomePage + "' WHERE SupplierID = " + sp.SupplierId + ";";
                sql = sql + " SELECT *  FROM Suppliers  WHERE SupplierID = " + sp.SupplierId.ToString();
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
                        x = new Suppliers
                        {
                            SupplierId = Int16.Parse(row["SupplierID"].ToString()),
                            CompanyName = row["CompanyName"].ToString(),
                            ContactName = row["ContactName"].ToString(),
                            ContactTitle = row["ContactTitle"].ToString(),
                            Address = row["Address"].ToString(),
                            City = row["City"].ToString(),
                            PostalCode = row["PostalCode"].ToString(),
                            Country = row["Country"].ToString(),
                            Phone = row["Phone"].ToString(),
                            Fax = row["Fax"].ToString(),
                            HomePage = row["HomePage"].ToString(),
                            Region = row["Region"].ToString()
                           
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


        


        #endregion
    }
}
