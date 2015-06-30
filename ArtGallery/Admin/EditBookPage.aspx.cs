using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace ArtGallery.DataLayer
{
    public partial class EditBookPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Event when user select upload picture button. It uploads the picture file to the
        /// App_Data directory and writes the file name inr the File Name text box and updates
        /// that column in the database.
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
                    BookDL.UpdatePicturePath((int)FormView1.DataKey.Value, filename);
                    FormView1.DataBind();
                }
                catch (Exception ex)
                {
                    ErrorLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
  
            }
            else
                ErrorLabel.Text = "Could not find file";

        }

        /// <summary>
        /// protects program against a user changing the query string to a nonexistance picture
        /// record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.ReturnValue == null || ((ArtGalleryDS.BookPage_GetByIdDataTable)e.ReturnValue).Count == 0)
                Response.Redirect("~/default.aspx", true);
            var table = (ArtGalleryDS.BookPage_GetByIdDataTable)e.ReturnValue;
            if(table != null && table.Rows.Count > 0)
            {
                var row = (ArtGalleryDS.BookPage_GetByIdRow) table[0];
                lblBook.Text = row.booktitle;
            }
        }

        #region log errors on record updates, inserts, and deletes
        protected void ObjectDataSource1_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                ErrorLogDL.Insert(e.Exception);
                e.ExceptionHandled = true;
                ErrorLabel.Text = "Update failed";
            }
            else
            {
                ErrorLabel.Text = "Update successful";

            }
            up2.Update();
        }

        protected void ObjectDataSource1_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            e.InputParameters.Remove("original_bookid");
        }
        #endregion

        #region next/previous
        /// <summary>
        /// This event is fired when user selects the previous button. It takes the user
        /// to previous record (alphabetically) in the current gallery.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrevious_Command(object sender, CommandEventArgs e)
        {
            int id = (int)FormView1.DataKey.Value;
            int newId = BookPageDL.Previous(id);
            if (newId > 0)
            {
                Response.Redirect("EditBookPage.aspx?id=" + newId.ToString());
            }
        }

        /// <summary>
        /// This event is fired when user selects the next button. It takes the user
        /// to next record (alphabetically) in the current gallery.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNext_Click(object sender, EventArgs e)
        {
            int id = (int)FormView1.DataKey.Value;
            int newId = BookPageDL.Next(id);
            if (newId > 0)
            {
                Response.Redirect("EditBookPage.aspx?id=" + newId.ToString());
            }
        }
        #endregion

    }
}