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
    /// Data Layer for WaterMark Table
    /// </summary>
    [DataObject( true )]
    public static class WaterMarkDL
    {
        /// <summary>
        /// Get all the records in WaterMark table as typed dataset
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod( DataObjectMethodType.Select, false )]
        public static ArtGalleryDS.WaterMarkDataTable Get()
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "WaterMark_Get", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter( selectCommand );
                data.EnforceConstraints = false;
                da.Fill( data, "WaterMark" );
            }
            catch (Exception ex)
            {
                string mess = ex.Message;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.WaterMark;
        }

        /// <summary>
        /// Update WaterMark Record
        /// </summary>
        /// <param name="WatermarkText">Text of watermark</param>
        /// <param name="WatermarkFontFamily">font for watermark</param>
        /// <param name="WatermarkFontColor">color of watermark</param>
        /// <param name="original_lastupdated">used for concurrency checking</param>
        /// <returns></returns>
        [DataObjectMethod( DataObjectMethodType.Update, true )]
        public static int Update(
            string WatermarkText,
            string WatermarkFontFamily,
            string WatermarkFontColor,
            string WatermarkFontSize,
            DateTime original_lastupdated
            )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "WaterMark_Update", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@WatermarkText", WatermarkText );
                selectCommand.Parameters.AddWithValue( "@WatermarkFontFamily", WatermarkFontFamily );
                selectCommand.Parameters.AddWithValue( "@WatermarkFontColor", WatermarkFontColor );
                selectCommand.Parameters.AddWithValue( "@WatermarkFontSize", WatermarkFontSize );
                selectCommand.Parameters.AddWithValue( "@original_lastupdated", original_lastupdated );
                return selectCommand.ExecuteNonQuery();
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