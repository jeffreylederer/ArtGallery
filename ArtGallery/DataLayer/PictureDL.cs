using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ArtGallery.DataLayer;
using System.IO;



namespace ArtGallery
{
    /// <summary>
    /// Data Layer for Picture table
    /// </summary>
    [DataObject(true)]
    public static class PictureDL
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ArtGalleryDS.PictureDataTable Get()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("Picture_Get", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(selectCommand);
                da.Fill(data, "Picture");
            }
            catch { }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.Picture;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ArtGalleryDS.PictureDataTable GetByGalleryId(int Galleryid)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("Picture_GetByGalleryId", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@Galleryid", Galleryid);
                SqlDataAdapter da = new SqlDataAdapter(selectCommand);
                da.Fill(data, "Picture");
            }
            catch { }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.Picture;
        }

        [DataObjectMethod( DataObjectMethodType.Select, false )]
        public static ArtGalleryDS.PictureDataTable GetByGalleryIdPublic( int Galleryid )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Picture_GetByGalleryIdPublic", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@Galleryid", Galleryid );
                SqlDataAdapter da = new SqlDataAdapter( selectCommand );
                da.Fill( data, "Picture" );
            }
            catch { }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.Picture;
        }
 


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ArtGalleryDS.PictureDataTable GetById(int id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("Picture_GetById", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@id",id);
                SqlDataAdapter da = new SqlDataAdapter(selectCommand);
                da.Fill(data, "Picture");
            }
            catch { }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.Picture;
        }

        [DataObjectMethod( DataObjectMethodType.Select, false )]
        public static ArtGalleryDS.Picture_GetWithWaterMarkDataTable GetWithWaterMark( int id )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Picture_GetWithWaterMark", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@id", id );
                SqlDataAdapter da = new SqlDataAdapter( selectCommand );
                da.Fill( data, "Picture_GetWithWaterMark" );
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
            return data.Picture_GetWithWaterMark;
        }


        public static ArtGalleryDS.PictureDataTable GetByIdTable( int id )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Picture_GetById", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@id", id );
                SqlDataAdapter da = new SqlDataAdapter( selectCommand );
                da.Fill( data, "Picture" );
            }
            catch { }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.Picture;
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public static int Update(
            string Title,
            string Frame,
            float Width,
            float Height,
            short Date,
            string MetaTags,
            string Notes,
            string Media,
            int GalleryId,
            string PicturePath,
            string surface,
            double price,
            decimal weight,
            string description,
            bool Available, 
            int original_id,
            string original_metatags,
            DateTime original_lastupdated
            )
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("Picture_Update", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@Title", Title);
                selectCommand.Parameters.AddWithValue("@Frame", Frame);
                selectCommand.Parameters.AddWithValue("@Width", Width);
                selectCommand.Parameters.AddWithValue("@Height", Height);
                selectCommand.Parameters.AddWithValue("@Date", Date);
                selectCommand.Parameters.AddWithValue("@MetaTags",MetaTags);
                selectCommand.Parameters.AddWithValue("@Notes", CheckNull(Notes));
                selectCommand.Parameters.AddWithValue("@Media", Media);
                selectCommand.Parameters.AddWithValue("@GalleryId", GalleryId);
                selectCommand.Parameters.AddWithValue("@PicturePath", PicturePath);
                selectCommand.Parameters.AddWithValue("@surface", CheckNull(surface));
                selectCommand.Parameters.AddWithValue("@price", price);
                selectCommand.Parameters.AddWithValue("@weight", weight);
                selectCommand.Parameters.AddWithValue("@description", description);
                selectCommand.Parameters.AddWithValue( "@Available", Available );
                selectCommand.Parameters.AddWithValue("@original_id", original_id);
                selectCommand.Parameters.AddWithValue("@original_metatags",original_metatags);
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
            string Title,
            string Frame,
            float Width,
            float Height,
            short Date,
            string MetaTags,
            string Notes,
            string Media,
            int GalleryId,
            string PicturePath,
            string surface,
            double price,
            decimal weight,
            string description,
            bool Available
           )
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("Picture_Insert", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@Title", Title);
                selectCommand.Parameters.AddWithValue("@Frame", Frame);
                selectCommand.Parameters.AddWithValue("@Width", Width);
                selectCommand.Parameters.AddWithValue("@Height", Height);
                selectCommand.Parameters.AddWithValue("@Date", Date);
                selectCommand.Parameters.AddWithValue("@MetaTags", CheckNull(MetaTags));
                selectCommand.Parameters.AddWithValue("@Notes", CheckNull(Notes));
                selectCommand.Parameters.AddWithValue("@Media", Media);
                selectCommand.Parameters.AddWithValue("@GalleryId", GalleryId);
                selectCommand.Parameters.AddWithValue("@PicturePath", PicturePath);
                selectCommand.Parameters.AddWithValue("@surface", CheckNull(surface));
                selectCommand.Parameters.AddWithValue("@price", price);
                selectCommand.Parameters.AddWithValue("@weight", weight);
                selectCommand.Parameters.AddWithValue("@description", CheckNull(description));
                selectCommand.Parameters.AddWithValue( "@Available", Available );
                SqlParameter id = selectCommand.Parameters.Add("@id", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                int rowcount=selectCommand.ExecuteNonQuery();
                if (rowcount > 0)
                    return (int) selectCommand.Parameters["@id"].Value;
                return 0;
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
        public static int Delete(int original_id, DateTime original_lastupdated, string original_metatags)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("Picture_Delete", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@original_id", original_id);
                selectCommand.Parameters.AddWithValue( "@original_lastupdated", original_lastupdated );
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


        public static int UpdatePicturePath( int id, string PicturePath )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Picture_UpdatePicturePath", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@id", id );
                selectCommand.Parameters.AddWithValue( "@PicturePath", PicturePath);
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



        public static int Next( int id )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Picture_Next", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@id", id );
                return (int)selectCommand.ExecuteScalar();
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

        public static int Previous( int id )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Picture_Previous", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@id", id );
                return (int) selectCommand.ExecuteScalar();
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

        public static int NextPublic( int id )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Picture_NextPublic", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@id", id );
                return (int)selectCommand.ExecuteScalar();
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

        public static int PreviousPublic( int id )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Picture_PreviousPublic", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@id", id );
                return (int)selectCommand.ExecuteScalar();
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

        private static object CheckNull(string val)
        {
            if (string.IsNullOrEmpty(val))
                return null;
            return val;
        }

        private static object CheckNull( decimal? val )
        {
            if (!val.HasValue)
                return null;
            return val;
        }

        private static object CheckNull( double? val )
        {
            if (!val.HasValue)
                return null;
            return val;
        }

        public static int Random()
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "dbo.Picture_Random", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                return (int) selectCommand.ExecuteScalar();
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

        public static int GetPictureCount
        {
            get
            {
                DataLayer.ArtGalleryDS.PictureDataTable table = ArtGallery.PictureDL.Get();
                return table.Count;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ArtGalleryDS.OrphanPictureDataTable GetOrphanPicture()
        {
            ArtGalleryDS.PictureDataTable table = PictureDL.Get();
            ArtGalleryDS.OrphanPictureDataTable missing = new ArtGalleryDS.OrphanPictureDataTable();
            string[] files = Directory.GetFiles( HttpContext.Current.Server.MapPath( "~/App_Data" ) );
            foreach (string file in files)
            {
                FileInfo fi = new FileInfo( file );
                string filename = fi.Name;
                if (filename == "Missing.jpg" || filename == "artgallery.p12" || filename == "paypal_cert_pem.txt")
                    continue; 
                var query = from p in table where filename.ToUpper() == p.PicturePath.ToUpper() select p.PicturePath;
                if (query.Count() == 0)
                {
                    object[] missingfile = new object[1];
                    missingfile[0] = filename;
                    missing.Rows.Add( missingfile );
                }

            }
            return missing;
        }

        [DataObjectMethod( DataObjectMethodType.Select, false )]
        public static ArtGalleryDS.Picture_GetInfoDataTable GetInfo( int id, bool repro )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Picture_GetInfo", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@id", id );
                selectCommand.Parameters.AddWithValue( "@repro", repro );
                SqlDataAdapter da = new SqlDataAdapter( selectCommand );
                da.Fill( data, "Picture_GetInfo" );
            }
            catch { }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.Picture_GetInfo;
        }

    }
}