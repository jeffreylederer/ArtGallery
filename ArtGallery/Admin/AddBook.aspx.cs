using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace ArtGallery.Admin
{
    public partial class AddBook : System.Web.UI.Page
    {
        
        
        /// <summary>
        /// Event when user select upload picture button. It uploads the picture file to the
        /// App_Data directory and writes the file name in the File Name text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName( FileUpload1.FileName );
                    FileUpload1.SaveAs( Server.MapPath( "~/App_Data/" ) + filename );
                    ErrorLabel.Text = "Upload status: File uploaded!";
                    ((TextBox)FormView1.FindControl( "lblPicturePath" )).Text = filename;
                }
                catch (Exception ex)
                {
                    ErrorLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
                up1.Update();
            }
            else
                ErrorLabel.Text = "Could not find file";

        }

        /// <summary>
        /// See comments
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        

        /// <summary>
        /// Assuming no error, after the insert occurs, the user is sent to the picture edit
        /// page for this newly inserted record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Inserted( object sender, ObjectDataSourceStatusEventArgs e )
        {
            
            if (e.Exception != null)
            {
                ErrorLogDL.Insert( e.Exception );
                e.ExceptionHandled = true;
                ErrorLabel.Text = "Insert failed";
                return;
            }
            string id = e.ReturnValue.ToString();
            Response.Redirect("EditBook.aspx?id=" + id);
        }

        
        protected void LinkCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx");
        }
   }
}