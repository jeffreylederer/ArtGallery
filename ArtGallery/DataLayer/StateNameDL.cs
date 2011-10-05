using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ArtGallery.DataLayer;



namespace ArtGallery
{
    /// <summary>
    /// Data Layer for StateName Table
    /// </summary>
    [DataObject( true )]
    public static class StateNameDL
    {
        /// <summary>
        /// Get all the records in Gallery table as typed dataset
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod( DataObjectMethodType.Select, false )]
        public static ArtGalleryDS.StateNameDataTable GetByCountry(string Country)
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "StateName_GetByCountry", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter( selectCommand );
                selectCommand.Parameters.AddWithValue( "@Country", Country );
                data.EnforceConstraints = false;
                da.Fill( data, "StateName" );
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
            return data.StateName;
        }
    }
}