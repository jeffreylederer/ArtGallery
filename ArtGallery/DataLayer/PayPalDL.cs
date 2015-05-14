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
    /// Data Layer for the PayPal Table
    /// </summary>
    [DataObject( true )]
    public static class PayPayDL
    {
        /// <summary>
        /// Get one record from PayPal table
        /// </summary>
        /// <param name="mode">either SandBox or Real</param>
        /// <returns>PayPal typed dataset</returns>
        [DataObjectMethod( DataObjectMethodType.Select, false )]
        public static ArtGalleryDS.PayPalDataTable GetByMode(string mode)
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            data.EnforceConstraints = false;
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "PayPal_GetByMode", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@mode", mode );
                SqlDataAdapter da = new SqlDataAdapter( selectCommand );
                da.Fill( data, "PayPal" );
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
            return data.PayPal;
        }

        /// <summary>
        /// Get one record from PayPal Table, the one that is active
        /// </summary>
        /// <returns>PayPal typed dataset</returns>
        public static ArtGalleryDS.PayPalRow GetAcive()
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            data.EnforceConstraints = false;
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "PayPal_GetActive", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter( selectCommand );
                da.Fill( data, "PayPal" );
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
            if (data.PayPal.Rows.Count == 1)
                return data.PayPal[0];
            return null;
        }

        /// <summary>
        /// Update a PayPal table record
        /// </summary>
        /// <param name="buynowurl">PayPal URL, one used for SandBox, the other
        /// for Real</param>
        /// <param name="BusinessEmailOrMerchantID">PayPal account name</param>
        /// <param name="active">Determine if active record</param>
        /// <param name="PDTAuthenticationToken">Notification ID</param>
        /// <param name="CertificateId"></param>
        /// <param name="PKCS12CertFile">Name of Certification file name in App_Data
        /// directory</param>
        /// <param name="PKCS12Password">Password for the Certification File</param>
        /// <param name="PayPalCertPath"></param>
        /// <param name="original_lastupdated">last time record was updated, used
        /// for concurrency detection</param>
        /// <param name="original_mode">Mode of record being updated</param>
        /// <returns></returns>
        [DataObjectMethod( DataObjectMethodType.Update, true )]
        public static int Update(
            string buynowurl,
            string BusinessEmailOrMerchantID,
            bool active,
            string PDTAuthenticationToken,
            string CertificateId,
            string PKCS12CertFile,
            string PKCS12Password,
            string PayPalCertPath,
            DateTime original_lastupdated,
            string original_mode
            )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "PayPal_Update", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@buynowurl", buynowurl );
                selectCommand.Parameters.AddWithValue( "@BusinessEmailOrMerchantID", BusinessEmailOrMerchantID );
                selectCommand.Parameters.AddWithValue( "@Active", active );
                selectCommand.Parameters.AddWithValue( "@PDTAuthenticationToken", PDTAuthenticationToken );
                selectCommand.Parameters.AddWithValue( "@CertificateId", CertificateId );
                selectCommand.Parameters.AddWithValue( "@PKCS12CertFile", PKCS12CertFile );
                selectCommand.Parameters.AddWithValue( "@PKCS12Password", PKCS12Password );
                selectCommand.Parameters.AddWithValue( "@PayPalCertPath", PayPalCertPath );
                selectCommand.Parameters.AddWithValue( "@original_lastupdated", original_lastupdated );
                selectCommand.Parameters.AddWithValue( "@original_mode", original_mode );
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

        /// <summary>
        /// Update the filename of PayPal encrypt button file and upload the file to
        /// App_Data directory
        /// </summary>
        /// <param name="PKCS12CertFile">File name</param>
        /// <param name="mode">either SandBox or Real</param>
        /// <returns></returns>
        public static int UpdatePKCS12CertPath(
            string PKCS12CertFile,
            string mode
            )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "PayPal_UpdatePKCS12CertPath", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@PKCS12CertFile", PKCS12CertFile );
                selectCommand.Parameters.AddWithValue( "@mode", mode );
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

       /// <summary>
       /// Update the filename of PayPal encrypt button file and upload the file to
       /// the App_Data diretory
       /// </summary>
       /// <param name="PayPalCertPath">file name</param>
       /// <param name="mode">either SandBox or Real</param>
       /// <returns></returns>
       public static int UpdatePayPalPath(
            string PayPalCertPath,
            string mode
            )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "PayPal_UpdatePayPalCertPath", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@PayPalCertPath", PayPalCertPath );
                selectCommand.Parameters.AddWithValue( "@mode", mode );
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