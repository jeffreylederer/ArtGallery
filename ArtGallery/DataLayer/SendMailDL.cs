using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ArtGallery.DataLayer;
using System.Net.Mail;


namespace ArtGallery
{
    /// <summary>
    /// Summary description for PictureDL
    /// </summary>
    [DataObject( true )]
    public static class SendMailDL
    {
        [DataObjectMethod( DataObjectMethodType.Select, false )]
        public static ArtGalleryDS.SendMailDataTable Get()
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "SendMail_Get", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter( selectCommand );
                da.Fill( data, "SendMail" );
            }
            catch { }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return data.SendMail;
        }

        [DataObjectMethod( DataObjectMethodType.Update, true )]
        public static int Update(
            bool EnableSSL,
            int Port,
            string Host,
            string emailaddress,
            string password,
            string EmailName,
            DateTime original_lastupdated
            )
        {
            SqlConnection conn = new SqlConnection( ConfigurationManager.ConnectionStrings["GalleryConnectionString"].ConnectionString );
            ArtGalleryDS data = new ArtGalleryDS();
            try
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand( "SendMail_Update", conn );
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue( "@EnableSSL", EnableSSL );
                selectCommand.Parameters.AddWithValue( "@Port", Port );
                selectCommand.Parameters.AddWithValue( "@Host", Host );
                selectCommand.Parameters.AddWithValue( "@emailaddress", emailaddress );
                selectCommand.Parameters.AddWithValue( "@EmailName", EmailName );
                selectCommand.Parameters.AddWithValue( "@password", password );
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

        public static bool SendMail( string to, string toname, string subject, string body, bool html )
        {
            ArtGalleryDS.SendMailDataTable table = Get();
            if (table == null || table.Rows.Count != 1)
                return false;

            ArtGalleryDS.SendMailRow row = table[0];
            MailMessage eMail = new System.Net.Mail.MailMessage( new MailAddress( row.emailaddress, row.EmailName ), new MailAddress( to, toname ) );
            eMail.Body = body;
            eMail.Subject = subject;
            eMail.IsBodyHtml = html;

            SmtpClient SMTP = new System.Net.Mail.SmtpClient();
            SMTP.EnableSsl = row.EnableSSL;
            SMTP.Port = row.Port;
            SMTP.Host = row.Host;
            SMTP.DeliveryMethod = SmtpDeliveryMethod.Network;
            SMTP.Credentials = new System.Net.NetworkCredential( row.emailaddress, row.password );
            try
            {
                SMTP.Send( eMail );
            }
            catch { return false; }
            return true;
        }

        public static bool SendMail( string subject, string body, bool html )
        {
            ArtGalleryDS.SendMailDataTable table = Get();
            if (table == null || table.Rows.Count != 1)
                return false;

            ArtGalleryDS.SendMailRow row = table[0];
            MailMessage eMail = new System.Net.Mail.MailMessage( new MailAddress( row.emailaddress, row.EmailName ), new MailAddress( row.emailaddress, row.EmailName ) );
            eMail.Body = body;
            eMail.Subject = subject;
            eMail.IsBodyHtml = html;

            SmtpClient SMTP = new System.Net.Mail.SmtpClient();
            SMTP.EnableSsl = row.EnableSSL;
            SMTP.Port = row.Port;
            SMTP.Host = row.Host;
            SMTP.DeliveryMethod = SmtpDeliveryMethod.Network;
            SMTP.Credentials = new System.Net.NetworkCredential( row.emailaddress, row.password );
            try
            {
                SMTP.Send( eMail );
            }
            catch { return false; }
            return true;
        }
    }
}