using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ArtGallery.DataLayer;



namespace ArtGallery
{
    /// <summary>
    /// Data Layer for SaleTax
    /// </summary>
    public static class SalesTaxDL
    {
        /// <summary>
        /// Calculates the cost of shipping
        /// </summary>
        /// <param name="weight">weight of the item in lbs</param>
        /// <param name="zipcode">zipcode of buyer</param>
        /// <param name="shiptype">type of shipping</param>
        /// <returns>cost of shipping</returns>
        public static decimal Get(string zipcode)
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();

            try
            {
                string zipStart = zipcode.Substring( 0, 5 );
                int zipCode = int.Parse( zipStart );
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "SalesTax_GetByZipCode", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@zip", zipCode );
                return (decimal) selectCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }
}