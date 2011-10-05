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
    /// Data Layer for UPSRate and UPSZone
    /// </summary>
    public static class UPSDL
    {
        /// <summary>
        /// Calculates the cost of shipping
        /// </summary>
        /// <param name="weight">weight of the item in lbs</param>
        /// <param name="zipcode">zipcode of buyer</param>
        /// <param name="shiptype">type of shipping</param>
        /// <returns>cost of shipping</returns>
        public static decimal Get( decimal weight, string zipcode, string shiptype )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();

            try
            {
                // first get the UPS shipping zone
                string zipStart = zipcode.Substring( 0, 3 );
                int zipPrefix = int.Parse( zipStart );
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "UpsZone_GetZone", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@zip", zipPrefix );
                string zone = selectCommand.ExecuteScalar().ToString();

                // next get the cost given the weight, type of shipping and zone
                SqlCommand selectCommand1 = new SqlCommand( "upsrate_GetByWeight", conn );
                selectCommand1.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter( selectCommand1 );
                selectCommand1.Parameters.AddWithValue( "@shiptype", shiptype );
                selectCommand1.Parameters.AddWithValue( "@weight", weight );
                data.EnforceConstraints = false;
                da.Fill( data, "upsrate" );
                if (data.upsrate.Rows.Count != 1)
                    throw new Exception( "No zone found" );
                decimal rate = (decimal)data.upsrate[0][zone];
                return rate;
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


