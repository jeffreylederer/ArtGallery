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
using System.Text.RegularExpressions;
using System.Text;

namespace ArtGallery
{
    /// <summary>
    /// Summary description for PictureDL
    /// </summary>
    [DataObject(true)]
    public static class SearchWordDL
    {
        /// <summary>
        /// Find a list of Picture with metatag words matching words in passed string
        /// </summary>
        /// <param name="searchString">a list of words separated by commas or blanks</param>
        /// <returns>a table of Picture.IDs with search words in metatags</returns>
        [DataObjectMethod( DataObjectMethodType.Select, false )]
        public static ArtGalleryDS.PictureDataTable Get(string searchString)
        {
            Regex rex = new Regex( "[A-Za-z0-9]+" );
            MatchCollection mc = rex.Matches( searchString );
            if (mc.Count == 0)
                return new ArtGalleryDS.PictureDataTable();
            StringBuilder query = new StringBuilder();
            query.Append( "select distinct pictureid as id, title, picturepath " );
            query.Append( "from PictureSearchWord ");
            query.Append( "inner join SearchWord on SearchWord.id = PictureSearchWord.searchwordid ");
            query.Append( "inner join Picture on picture.id = PictureSearchWord.pictureid where ");
            for (int i = 0; i < mc.Count; i++)
            {
                if (i == mc.Count - 1)
                    query.AppendFormat( "Contains(searchterm, '{0}')", mc[i].Value );
                else
                    query.AppendFormat( "Contains(searchterm, '{0}') or ", mc[i].Value );
            }
            string commandtext = query.ToString();
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( commandtext, conn );
                selectCommand.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter( selectCommand );
                da.Fill( data, "Picture" );
            }
            catch(Exception ex)
            {
                string message = ex.Message;
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.Picture;
        }

        /// <summary>
        /// looks up a list of ids to make sure that match Picture.id values
        /// </summary>
        /// <param name="searchString">a list of numbers separated by commas</param>
        /// <returns>a table of id found in picture table</returns>
        public static ArtGalleryDS.PictureDataTable GetByList( string searchString )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "Picture_GetByList", conn );
                selectCommand.Parameters.AddWithValue( "@list", searchString );
                selectCommand.CommandType = CommandType.StoredProcedure;
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

        /// <summary>
        /// Find all the metatags in all the picture metatags, not used anymore
        /// </summary>
        /// <returns></returns>
        public static string GetList()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            StringBuilder str = new StringBuilder();
            SqlDataReader reader = null;
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("SearchWord_GetList", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    str.Append(reader.GetString(0));
                    str.Append(",");
                }
             }
            catch { }
            finally
            {
                if(reader != null && !reader.IsClosed)
                    reader.Close();
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return str.ToString().Substring(0,str.Length-1);
        }
    }
}