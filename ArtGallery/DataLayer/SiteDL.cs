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
    public static class SiteDL
    {
        [DataObjectMethod( DataObjectMethodType.Select, false )]
        public static ArtGalleryDS.SiteDataTable Get()
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Site_Get", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter( selectCommand );
                da.Fill( data, "Site" );
            }
            catch { }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.Site;
        }

        [DataObjectMethod( DataObjectMethodType.Select, false )]
        public static ArtGalleryDS.SiteDataTable GetSite()
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            data.EnforceConstraints = false;
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Site_GetSite", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter( selectCommand );
                da.Fill( data, "Site" );
            }
            catch { }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.Site;
        }

        [DataObjectMethod( DataObjectMethodType.Select, false )]
        public static ArtGalleryDS.SiteDataTable GetArtist()
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            data.EnforceConstraints = false;
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Site_GetArtist", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter( selectCommand );
                da.Fill( data, "Site" );
            }
            catch { }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.Site;
        }

        [DataObjectMethod( DataObjectMethodType.Update, true )]
        public static int Update(
            string email,
            string LogoPath,
            string ArtistImagePath,
            string HomePageText,
            string ArtistPageText,
            string Metatags,
            string contact,
            string copyrightholder
            )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Site_Update", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@email", email );
                selectCommand.Parameters.AddWithValue( "@LogoPath", LogoPath );
                selectCommand.Parameters.AddWithValue( "@ArtistImagePath", ArtistImagePath );
                selectCommand.Parameters.AddWithValue( "@HomePageText", HomePageText );
                selectCommand.Parameters.AddWithValue( "@ArtistPageText", ArtistPageText );
                selectCommand.Parameters.AddWithValue( "@Metatags", Metatags );
                selectCommand.Parameters.AddWithValue( "@contact", contact );
                selectCommand.Parameters.AddWithValue( "@copyrightholder", copyrightholder );
                int retval = selectCommand.ExecuteNonQuery();
                HttpContext.Current.Application["metatags"] = Metatags;
                HttpContext.Current.Application["logo"] = LogoPath;
                return retval;
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

        [DataObjectMethod( DataObjectMethodType.Update, true )]
        public static int UpdateArtist(
            string email,
            string ArtistImagePath,
            string ArtistPageText,
            string contact
            )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Site_UpdateArtist", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@email", email );
                selectCommand.Parameters.AddWithValue( "@ArtistImagePath", ArtistImagePath );
                selectCommand.Parameters.AddWithValue( "@ArtistPageText", ArtistPageText );
                selectCommand.Parameters.AddWithValue( "@contact", contact );
                int retval = selectCommand.ExecuteNonQuery();
                return retval;
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

        [DataObjectMethod( DataObjectMethodType.Update, true )]
        public static int UpdateSite(
            string LogoPath,
            string HomePageText,
            string Metatags,
            string copyrightholder
            )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Site_UpdateSite", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@LogoPath", LogoPath );
                selectCommand.Parameters.AddWithValue( "@HomePageText", HomePageText );
                selectCommand.Parameters.AddWithValue( "@Metatags", Metatags );
                selectCommand.Parameters.AddWithValue( "@copyrightholder", copyrightholder );
                int retval = selectCommand.ExecuteNonQuery();
                HttpContext.Current.Application["metatags"] = Metatags;
                HttpContext.Current.Application["logo"] = LogoPath;
                return retval;
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

        public static int UpdateArtistImagePath(
            string ArtistImagePath
            )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Site_UpdateArtistImagePath", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@ArtistImagePath", ArtistImagePath );
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

        public static int UpdateLogoPath(
            string LogoPath
            )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Site_UpdateLogoPath", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@LogoPath", LogoPath );
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

        public static string GetLogo()
        {
            ArtGalleryDS.SiteDataTable table = Get();
            return table[0].LogoPath;
        }
    }
}