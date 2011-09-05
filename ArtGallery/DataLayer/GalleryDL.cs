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
    [DataObject(true)]
    public static class GalleryDL
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ArtGalleryDS.GalleryDataTable Get()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("Gallery_Get", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(selectCommand);
                da.Fill(data, "Gallery");
            }
            catch { }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.Gallery;
        }

        [DataObjectMethod( DataObjectMethodType.Select, false )]
        public static ArtGalleryDS.GalleryDataTable GetPublic()
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Gallery_GetPublic", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter( selectCommand );
                da.Fill( data, "Gallery" );
            }
            catch { }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.Gallery;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ArtGalleryDS.GalleryDataTable GetById(int id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            data.EnforceConstraints = false;
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("Gallery_GetById", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@id", id);
                SqlDataAdapter da = new SqlDataAdapter(selectCommand);
                da.Fill(data, "Gallery");
            }
            catch { }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.Gallery;
        }

        [DataObjectMethod( DataObjectMethodType.Select, false )]
        public static ArtGalleryDS.GalleryDataTable GetByIdPublic( int id )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            data.EnforceConstraints = false;
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Gallery_GetByIdPublic", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@id", id );
                SqlDataAdapter da = new SqlDataAdapter( selectCommand );
                da.Fill( data, "Gallery" );
            }
            catch { }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.Gallery;
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public static int Update(
            string menutext,
            string gallerytitle,
            bool active,
            int original_id,
            DateTime original_lastupdated
            )
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("Gallery_Update", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@menutext", menutext);
                selectCommand.Parameters.AddWithValue("@gallerytitle", gallerytitle);
                selectCommand.Parameters.AddWithValue( "@active", active );
                selectCommand.Parameters.AddWithValue("@original_id", original_id);
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

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public static int Insert(
            string menutext,
            string gallerytitle,
            bool active)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("Gallery_Insert", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@menutext", menutext);
                selectCommand.Parameters.AddWithValue("@gallerytitle", gallerytitle);
                selectCommand.Parameters.AddWithValue( "@active", active );
                SqlParameter id=selectCommand.Parameters.Add("@id", SqlDbType.Int, 4);
                id.Direction = ParameterDirection.Output;

                int retval =  selectCommand.ExecuteNonQuery();
                if (retval != 1)
                    throw new Exception("No record inserted");
                return (int) id.Value;
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

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public static int Delete(int original_id, DateTime original_lastupdated)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("Gallery_Delete", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@original_id", original_id);
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