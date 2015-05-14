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
    /// Data Layer for ErrorLog table
    /// </summary>
    [DataObject( true )]
    public static class ErrorLogDL
    {
        /// <summary>
        /// Get a list of error in the ErrorLog table
        /// </summary>
        /// <returns>typed dataset of errors</returns>
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

        /// <summary>
        /// Return one error
        /// </summary>
        /// <param name="id">the row number of the error</param>
        /// <returns>typed dataset containing the error</returns>
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

        /// <summary>
        /// Insert error into ErrorLog table
        /// </summary>
        /// <param name="ex"></param>
        /// <returns>number of rows inserted</returns>
        [DataObjectMethod( DataObjectMethodType.Insert, true )]
        public static int Insert(Exception ex)
        {
            if (ex == null)
                return 0;
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
                selectCommand.Parameters.AddWithValue( "@errormessage", str.ToString() );
                SqlParameter id=selectCommand.Parameters.Add("@id", SqlDbType.Int, 4);
                id.Direction = ParameterDirection.Output;

                selectCommand.ExecuteNonQuery();
                return (int) id.Value;
               
            }
            catch(Exception ex1) 
            {
                string message = ex1.Message;
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