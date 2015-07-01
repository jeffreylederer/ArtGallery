using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace ArtGallery.Admin
{
    public partial class AddBookPage : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idStr = Request.QueryString["id"];
                if (string.IsNullOrWhiteSpace(idStr))
                    Response.Redirect("default.aspx");
                int id = 0;
                if (!int.TryParse(idStr, out id))
                    Response.Redirect("default.aspx");
            }
        }
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
                    string filename = Path.GetFileName(FileUpload1.FileName);
                    FileUpload1.SaveAs(Server.MapPath("~/App_Data/") + filename);
                    ErrorLabel.Text = "Upload status: File uploaded!";
                    ((TextBox)FormView1.FindControl("lblPicturePath")).Text = filename;
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

        protected void Cancel_Click(object sender, EventArgs e)
        {
            var id = Request.QueryString["id"];
            Response.Redirect("EditBook.aspx?id=" + id, true);
        }

        protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                ErrorLogDL.Insert(e.Exception);
                e.ExceptionHandled = true;
                ErrorLabel.Text = "Insert failed";
                return;
            }
            var id = e.ReturnValue.ToString();
            Response.Redirect("EditBookPage.aspx?id=" + id, true);
        }
    }
}