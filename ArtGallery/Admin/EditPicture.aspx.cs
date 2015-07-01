using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtGallery.DataLayer;
using System.IO;



namespace ArtGallery
{
    public partial class EditPicture : System.Web.UI.Page
    {

        /// <summary>
        /// Do not why it is necessary, but most rebind the picture. Probably the picture handler
        /// control does not support viewstate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
             FormView2.DataBind();
        }

        /// <summary>
        /// Event when user select upload picture button. It uploads the picture file to the
        /// App_Data directory and writes the file name inr the File Name text box and updates
        /// that column in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UploadButton_Click( object sender, EventArgs e )
        {
            if (FileUpload1.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName( FileUpload1.FileName );
                    FileUpload1.SaveAs( Server.MapPath( "~/App_Data/" ) + filename );
                    ErrorLabel.Text = "Upload status: File uploaded!";
                    PictureDL.UpdatePicturePath((int) FormView1.DataKey.Value, filename );
                    FormView1.DataBind();
                }
                catch (Exception ex)
                {
                    ErrorLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
                up2.Update();
            }
            else
                ErrorLabel.Text = "Could not find file";
        }


        /// <summary>
        /// This event is fired when user selects the previous button. It takes the user
        /// to previous record (alphabetically) in the current gallery.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrevious_Command( object sender, CommandEventArgs e )
        {
            int id = (int)FormView1.DataKey.Value;
            int galleryid = int.Parse( Request.QueryString["id"] );
            int newId = PictureDL.Previous( id );
            if (newId > 0)
            {
                Response.Redirect( "EditPicture.aspx?id=" + newId.ToString() );
            }
        }

        /// <summary>
        /// This event is fired when user selects the next button. It takes the user
        /// to next record (alphabetically) in the current gallery.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNext_Click( object sender, EventArgs e )
        {
            int id = (int)FormView1.DataKey.Value;
            int galleryid = int.Parse( Request.QueryString["id"] );
            int newId = PictureDL.Next( id );
            if (newId > 0)
            {
                Response.Redirect( "EditPicture.aspx?id=" + newId.ToString() );
            }
        }

        /// <summary>
        /// Sets the column values (from the grid) for the insert stored procedure call.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsReproduction_Inserting( object sender, ObjectDataSourceMethodEventArgs e )
        {
            TextBox handling = (TextBox)GridView1.FooterRow.FindControl( "txtHandling" ) as TextBox;
            TextBox Packing = (TextBox)GridView1.FooterRow.FindControl( "txtPacking" ) as TextBox;
            e.InputParameters["pictureid"] = (int) FormView1.DataKey.Value;
            e.InputParameters["description"] = ((TextBox)GridView1.FooterRow.FindControl( "txtDescription" )).Text;
            e.InputParameters["price"] = decimal.Parse( ((TextBox)GridView1.FooterRow.FindControl( "txtPrice" )).Text );
            e.InputParameters["weight"] = decimal.Parse( ((TextBox)GridView1.FooterRow.FindControl( "txtWeight" )).Text );
            e.InputParameters["width"] = decimal.Parse( ((TextBox)GridView1.FooterRow.FindControl( "txtWidth" )).Text );
            e.InputParameters["height"] = decimal.Parse( ((TextBox)GridView1.FooterRow.FindControl( "txtHeight" )).Text );
        }

        /// <summary>
        /// protects program against a user changing the query string to a nonexistance picture
        /// record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selected( object sender, ObjectDataSourceStatusEventArgs e )
        {
            if (e.ReturnValue == null || ((ArtGalleryDS.Picture_GetByIDDataTable)e.ReturnValue).Rows.Count == 0)
                Response.Redirect( "~/default.aspx", true );
        }

        #region log errors on record updates, inserts, and deletes
        protected void ObjectDataSource1_Updated( object sender, ObjectDataSourceStatusEventArgs e )
        {
            if (e.Exception != null)
            {
                ErrorLogDL.Insert(e.Exception);
                e.ExceptionHandled = true;
                ErrorLabel.Text = "Update failed";
            }
            else
                ErrorLabel.Text = "Updated successful";
        }

        protected void ObjectDataSource1_Deleted( object sender, ObjectDataSourceStatusEventArgs e )
        {
            if (e.Exception != null)
            {
                ErrorLogDL.Insert(e.Exception);
                e.ExceptionHandled = true;
                ErrorLabel.Text = "Delete failed";
            }
            else
                Response.Redirect("default.aspx");
        }

        protected void odsReproductions_Inserted( object sender, ObjectDataSourceStatusEventArgs e )
        {
            if (e.Exception != null)
            {
                ErrorLogDL.Insert(e.Exception);
                e.ExceptionHandled = true;
                ErrorLabel.Text = "Insert failed";
            }
            else
                ErrorLabel.Text = "Insert successful";
        }

        protected void odsReproductions_Updated( object sender, ObjectDataSourceStatusEventArgs e )
        {
            if (e.Exception != null)
            {
                ErrorLogDL.Insert(e.Exception);
                e.ExceptionHandled = true;
                ErrorLabel.Text = "Update failed";
            }
            else
                ErrorLabel.Text = "Updated successful";
        }

        protected void odsReproductions_Deleted( object sender, ObjectDataSourceStatusEventArgs e )
        {
            if (e.Exception != null)
            {
                ErrorLogDL.Insert(e.Exception);
                e.ExceptionHandled = true;
                ErrorLabel.Text = "Deleet failed";
            }
            else
                ErrorLabel.Text = "Delete successful";
        }
        #endregion
    }
}