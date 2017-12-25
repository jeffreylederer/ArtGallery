using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ArtGallery.DataLayer;


namespace ArtGallery
{
    [DataObject(true)]   
    public static class BookPageDL
    {
        

        /// <summary>
        /// Get on particular BookPage record as typed dataset
        /// </summary>
        /// <param name="id">row number of that record</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ArtGalleryDS.BookPage_GetByIdDataTable GetById(int id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            data.EnforceConstraints = false;
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("BookPage_GetById", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@id", id);
                SqlDataAdapter da = new SqlDataAdapter(selectCommand);
                da.Fill(data, "BookPage_GetById");
            }
            catch (Exception)
            { 
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.BookPage_GetById;
        }

         [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ArtGalleryDS.BookPageDataTable Get()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            data.EnforceConstraints = false;
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("BookPage_Get", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(selectCommand);
                da.Fill(data, "BookPage");
            }
            catch { }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.BookPage;
        } 
        
        /// <summary>
        /// Get on particular BookPage record as typed dataset
        /// </summary>
        /// <param name="id">row number of that record</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ArtGalleryDS.BookPageDataTable GetByIdPublic(int id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            data.EnforceConstraints = false;
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("BookPage_GetByIdPublic", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@id", id);
                SqlDataAdapter da = new SqlDataAdapter(selectCommand);
                da.Fill(data, "BookPage");
            }
            catch { }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.BookPage;
        }


        /// <summary>
        /// Get on particular BookPage record as typed dataset
        /// </summary>
        /// <param name="id">row number of that record</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ArtGalleryDS.BookPageDataTable GetByBookId(int bookid)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            data.EnforceConstraints = false;
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("BookPage_GetByBookId", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@bookid", bookid);
                SqlDataAdapter da = new SqlDataAdapter(selectCommand);
                da.Fill(data, "BookPage");
            }
            catch(Exception) 
            { 
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.BookPage;
        } /// <summary>
        /// Get on particular BookPage record as typed dataset
        /// </summary>
        /// <param name="id">row number of that record</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ArtGalleryDS.BookPageDataTable GetByBookIdPublic(int bookid)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            data.EnforceConstraints = false;
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("BookPage_GetByBookIdPublic", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@bookid", bookid);
                SqlDataAdapter da = new SqlDataAdapter(selectCommand);
                da.Fill(data, "BookPage");
            }
            catch (Exception)
            {
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.BookPage;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ArtGalleryDS.BookPage_GetWithWaterMarkDataTable GetWithWaterMark(int id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("BookPage_GetWithWaterMark", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@id", id);
                SqlDataAdapter da = new SqlDataAdapter(selectCommand);
                da.Fill(data, "BookPage_GetWithWaterMark");
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
            return data.BookPage_GetWithWaterMark;
        }

        /// <summary>
        /// Update one record in BookPage table
        /// </summary>
        /// <param name="menutext">menu column</param>
        /// <param name="BookPagetitle">BookPagetitle column</param>
        /// <param name="active">determine if record is available to public</param>
        /// <param name="original_id">id of record in formview</param>
        /// <param name="original_lastupdated">last time record was updated, used
        /// for concurrency detection</param>
        /// <returns>number of records updated</returns>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public static int Update(
            string PicturePath,
            string title,
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
                SqlCommand selectCommand = new SqlCommand("BookPage_Update", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@PicturePath", PicturePath);
                selectCommand.Parameters.AddWithValue("@title", title);
                selectCommand.Parameters.AddWithValue("@active", active);
                selectCommand.Parameters.AddWithValue("@original_id", original_id);
                selectCommand.Parameters.AddWithValue("@original_lastupdated", original_lastupdated);
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
        /// Insert new record in BookPage Table
        /// </summary>
        /// <param name="menutext">menu column</param>
        /// <param name="BookPagetitle">BookPagetitle column</param>
        /// <param name="active">determine if record is available to public</param>
        /// <returns>the id for that record</returns>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public static int Insert(
            int bookid,
            string PicturePath,
            string title,
            bool active)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("BookPage_Insert", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@bookid", bookid);
                selectCommand.Parameters.AddWithValue("@PicturePath", PicturePath);
                selectCommand.Parameters.AddWithValue("@title", title);
                selectCommand.Parameters.AddWithValue("@active", active);
                SqlParameter id = selectCommand.Parameters.Add("@id", SqlDbType.Int, 4);
                id.Direction = ParameterDirection.Output;

                int retval = selectCommand.ExecuteNonQuery();
                if (retval != 1)
                    throw new Exception("No record inserted");
                return (int)id.Value;
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
        /// Delete a recod from the BookPage Table
        /// </summary>
        /// <param name="original_id">id of record in formview</param>
        /// <param name="original_lastupdated">last time record was updated, used
        /// for concurrency detection</param>
        /// <returns>number of records removed</returns>
        /// <remarks>Will fail if there are pictures in the BookPage</remarks>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public static int Delete(int original_id, DateTime original_lastupdated)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("BookPage_Delete", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@original_id", original_id);
                selectCommand.Parameters.AddWithValue("@original_lastupdated", original_lastupdated);
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

        public static int Next(int id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("BookPage_Next", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@id", id);
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

        public static int Previous(int id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("BookPage_Previous", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@id", id);
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

        public static int NextPublic(int id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("BookPage_NextPublic", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@id", id);
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

        public static int PreviousPublic(int id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("BookPage_PreviousPublic", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@id", id);
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
    }
}