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

    public class ProductsRep : GenericRep<NorthwindContext, Products>
    {
        #region -- Overrides --

       

        #endregion

        #region -- Methods --

        

        //Đề 5 Câu 3
        public Products SP_InsertProducts(Products pro)
        {
            Products x = new Products();
            var cnn = (SqlConnection)Context.Database.GetDbConnection();
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();
            try
            {
                string sql = "INSERT INTO [Products] ([ProductName],[SupplierID],[CategoryID]" +
                    ",[QuantityPerUnit],[UnitPrice],[UnitsInStock],[UnitsOnOrder],[ReorderLevel],[Discontinued])" +
                    "  VALUES('" + pro.ProductName + "'," + pro.SupplierId + "," + pro.CategoryId + ",'" 
                    + pro.QuantityPerUnit + "'," + pro.UnitPrice + "," + pro.UnitsInStock + "," + pro.UnitsOnOrder + "," 
                    + pro.ReorderLevel + ", " +  (pro.Discontinued?"1":"0") + ");";
                sql = sql + " SELECT * FROM [Products] WHERE ProductID = @@IDENTITY";
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
                        x = new Products
                        {
                            ProductId = Int16.Parse(row["ProductID"].ToString()),
                            SupplierId = Int16.Parse(row["SupplierID"].ToString()),
                            CategoryId = Int16.Parse(row["CategoryID"].ToString()),
                            ProductName = row["ProductName"].ToString(),
                            QuantityPerUnit = row["QuantityPerUnit"].ToString(),
                            UnitPrice = Decimal.Parse(row["UnitPrice"].ToString()),
                            UnitsInStock = Int16.Parse(row["UnitsInStock"].ToString()),
                            UnitsOnOrder = Int16.Parse(row["UnitsOnOrder"].ToString()),
                            ReorderLevel = Int16.Parse(row["ReorderLevel"].ToString()),
                            Discontinued = bool.Parse(row["Discontinued"].ToString())
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
