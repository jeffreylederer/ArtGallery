using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace ArtGallery.Admin
{
    public partial class AddPicture : System.Web.UI.Page
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
        protected void FormView1_PreRender( object sender, EventArgs e )
        {
            if (!IsPostBack)
            {
                // set available checkbox to true
                ((CheckBox)FormView1.FindControl( "chkAvailble" )).Checked = true;

                // check is user got her via picture edit page and user
                // selected add new picture to same gallery. Then it selects
                // in the gallery dropdown, the same gallery.
                string idStr = Request.QueryString["id"];
                if (string.IsNullOrWhiteSpace( idStr ))
                    return;
                int id;
                if (!int.TryParse( idStr, out id ))
                    return;
                DropDownList DropDownList1 = FormView1.FindControl("DropDownList1") as DropDownList;
                if (id < 0 || id > DropDownList1.Items.Count - 1)
                    return;
                DropDownList1.SelectedIndex = id;
            }
        }

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
                return;
            }
            string id = e.ReturnValue.ToString();
            ViewState["id"] = id;
        }

        protected void FormView1_ItemInserted( object sender, FormViewInsertedEventArgs e )
        {
            if (e.Exception != null)
            {
                ErrorLogDL.Insert( e.Exception );
                e.ExceptionHandled = true;
                return;
            }
            string id = (string)ViewState["id"];
            Response.Redirect( "EditPicture.aspx?id=" + id, true );
        }

               
        

   }
}