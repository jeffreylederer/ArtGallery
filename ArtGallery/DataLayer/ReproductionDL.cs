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
    /// Summary description for PictureDL
    /// </summary>
    [DataObject( true )]
    public static class ReproductionDL
    {
        [DataObjectMethod( DataObjectMethodType.Select, false )]
        public static ArtGalleryDS.ReproductionDataTable GetByPictureId( int pictureid )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Reproduction_GetByPictureID", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@pictureid", pictureid );
                SqlDataAdapter da = new SqlDataAdapter( selectCommand );
                da.Fill( data, "Reproduction" );
            }
            catch { }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.Reproduction;
        }

        public static ArtGalleryDS.ReproductionDataTable GetById( int id )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Reproduction_GetById", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@id", id );
                SqlDataAdapter da = new SqlDataAdapter( selectCommand );
                da.Fill( data, "Reproduction" );
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
            return data.Reproduction;
        }


        [DataObjectMethod( DataObjectMethodType.Update, true )]
        public static int Update(
            string description,
            decimal price,
            decimal weight,
            decimal? handling,
            decimal width,
            decimal height,
            double? packingweight,
            int original_id,
            DateTime original_lastupdated
            )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Reproduction_Update", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@description",  description);
                selectCommand.Parameters.AddWithValue( "@price", price );
                selectCommand.Parameters.AddWithValue( "@width",  width);
                selectCommand.Parameters.AddWithValue( "@height", height );
                selectCommand.Parameters.AddWithValue( "@weight", weight );
                selectCommand.Parameters.AddWithValue( "@original_id", original_id );
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

        [DataObjectMethod( DataObjectMethodType.Insert, true )]
        public static int Insert(
            int pictureid,
            string description,
            decimal price,
            decimal weight,
            decimal width,
            decimal height
        )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Reproduction_Insert", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@description", description );
                selectCommand.Parameters.AddWithValue( "@price", price );
                selectCommand.Parameters.AddWithValue( "@width", width );
                selectCommand.Parameters.AddWithValue( "@height", height );
                selectCommand.Parameters.AddWithValue( "@weight", weight );
                selectCommand.Parameters.AddWithValue( "@pictureid", pictureid );
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

        [DataObjectMethod( DataObjectMethodType.Delete, true )]
        public static int Delete( int original_id, DateTime original_lastupdated )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Reproduction_Delete", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@original_id", original_id );
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