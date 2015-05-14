﻿using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ArtGallery.DataLayer;



namespace ArtGallery
{
    /// <summary>
    /// Data Layer for Gallery Table
    /// </summary>
    [DataObject(true)]
    public static class BookDL
    {
        /// <summary>
        /// Get all the records in Book table as typed dataset
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ArtGalleryDS.BookDataTable Get()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("Book_Get", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(selectCommand);
                da.Fill(data, "Book");
            }
            catch { }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.Book;
        }

        /// <summary>
        /// Get all the records in Book table as typed dataset visible to public
        /// i.e. avaiable and have picture that can be displayed
        /// </summary>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ArtGalleryDS.BookDataTable GetPublic(int active)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("Book_GetPublic", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@active", active==1);
                SqlDataAdapter da = new SqlDataAdapter(selectCommand);
                da.Fill(data, "Book");
            }
            catch { }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.Book;
        }

        /// <summary>
        /// Get on particular Book record as typed dataset
        /// </summary>
        /// <param name="id">row number of that record</param>
        /// <returns></returns>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ArtGalleryDS.BookDataTable GetById(int id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            data.EnforceConstraints = false;
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("Book_GetById", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@id", id);
                SqlDataAdapter da = new SqlDataAdapter(selectCommand);
                da.Fill(data, "Book");
            }
            catch { }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.Book;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ArtGalleryDS.Picture_GetWithWaterMarkDataTable GetWithWaterMark(int id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("Book_GetWithWaterMark", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@id", id);
                SqlDataAdapter da = new SqlDataAdapter(selectCommand);
                da.Fill(data, "Picture_GetWithWaterMark");
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
        

        /// <summary>
        /// Update one record in Book table
        /// </summary>
        /// <param name="menutext">menu column</param>
        /// <param name="Booktitle">Booktitle column</param>
        /// <param name="active">determine if record is available to public</param>
        /// <param name="original_id">id of record in formview</param>
        /// <param name="original_lastupdated">last time record was updated, used
        /// for concurrency detection</param>
        /// <returns>number of records updated</returns>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public static int Update(
            string menutext,
            string booktitle,
            string bookstyle,
            double price,
            decimal packingweight,
            string description,
            string PicturePath,
            short Date,
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
                SqlCommand selectCommand = new SqlCommand("Book_Update", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@menutext", menutext);
                selectCommand.Parameters.AddWithValue("@booktitle", booktitle);
                selectCommand.Parameters.AddWithValue("@bookstyle", bookstyle);
                selectCommand.Parameters.AddWithValue("@price", price);
                selectCommand.Parameters.AddWithValue("@packingweight", packingweight);
                selectCommand.Parameters.AddWithValue("@description", description);
                selectCommand.Parameters.AddWithValue("@PicturePath", PicturePath);
                selectCommand.Parameters.AddWithValue("@Date", Date);
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
        /// Insert new record in Book Table
        /// </summary>
        /// <param name="menutext">menu column</param>
        /// <param name="Booktitle">Booktitle column</param>
        /// <param name="active">determine if record is available to public</param>
        /// <returns>the id for that record</returns>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public static int Insert(
            string menutext,
            string booktitle,
            string bookstyle,
            double price,
            decimal packingweight,
            string description,
            string PicturePath,
            short Date,
            bool active)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("Book_Insert", conn);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@menutext", menutext);
                selectCommand.Parameters.AddWithValue("@booktitle", booktitle);
                selectCommand.Parameters.AddWithValue("@bookstyle", bookstyle);
                selectCommand.Parameters.AddWithValue("@price", price);
                selectCommand.Parameters.AddWithValue("@packingweight", packingweight);
                selectCommand.Parameters.AddWithValue("@description", description);
                selectCommand.Parameters.AddWithValue("@PicturePath", PicturePath);
                selectCommand.Parameters.AddWithValue("@Date", Date);
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
        /// Delete a recod from the Book Table
        /// </summary>
        /// <param name="original_id">id of record in formview</param>
        /// <param name="original_lastupdated">last time record was updated, used
        /// for concurrency detection</param>
        /// <returns>number of records removed</returns>
        /// <remarks>Will fail if there are pictures in the Book</remarks>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public static int Delete(int original_id, DateTime original_lastupdated)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString);
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("Book_Delete", conn);
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
    }
}