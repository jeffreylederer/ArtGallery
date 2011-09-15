using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ArtGallery.DataLayer;
using System.Text;



namespace ArtGallery
{
    /// <summary>
    /// Summary description for PictureDL
    /// </summary>
    [DataObject( true )]
    public static class ErrorLogDL
    {
        [DataObjectMethod( DataObjectMethodType.Select, false )]
        public static ArtGalleryDS.ErrorLogDataTable Get()
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "ErrorLog_Get", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter( selectCommand );
                da.Fill( data, "ErrorLog" );
            }
            catch { }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.ErrorLog;
        }

        [DataObjectMethod( DataObjectMethodType.Select, false )]
        public static ArtGalleryDS.ErrorLogDataTable GetById(int id)
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "ErrorLog_GetById", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@id", id );
                SqlDataAdapter da = new SqlDataAdapter( selectCommand );
                da.Fill( data, "ErrorLog" );
            }
            catch { }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.ErrorLog;
        }

        [DataObjectMethod( DataObjectMethodType.Insert, true )]
        public static int Insert(Exception ex)
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            StringBuilder str = new StringBuilder();
            while (ex != null)
            {
                str.AppendLine( ex.Message );
                str.AppendLine( ex.StackTrace );
                str.AppendLine( "------------------------------------" );
                ex = ex.InnerException;
            }
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "ErrorLog_Insert", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@message", str.ToString() );
                SqlParameter id=selectCommand.Parameters.Add("@id", SqlDbType.Int, 4);
                id.Direction = ParameterDirection.Output;

                int retval =  selectCommand.ExecuteNonQuery();
                if (retval != 1)
                    throw new Exception("No record inserted");
                return (int) id.Value;
            }
            catch 
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