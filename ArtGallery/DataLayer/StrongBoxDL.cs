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
    /// Data Layer for StrongBox table
    /// </summary>
    [DataObject( true )]
    public static class StrongBoxDL
    {
        /// <summary>
        /// Get all the records of the StrongBox table
        /// </summary>
        /// <returns>typed dataset</returns>
        [DataObjectMethod( DataObjectMethodType.Select, false )]
        public static ArtGalleryDS.StrongBoxDataTable Get()
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "StrongBox_Get", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter( selectCommand );
                da.Fill( data, "StrongBox" );
            }
            catch { }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.StrongBox;
        }
    }
}