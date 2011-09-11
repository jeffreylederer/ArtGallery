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
        protected void Page_Load(object sender, EventArgs e)
        {
             FormView2.DataBind();
        }

 
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
        }

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

        protected void ObjectDataSource1_Updated( object sender, ObjectDataSourceStatusEventArgs e )
        {
            if (e.Exception == null && (int)e.ReturnValue == 1)
            {
                ErrorLabel.Text = "Saved Successfully";
                up1.Update();
            }
        }

        protected void ObjectDataSource1_Selected( object sender, ObjectDataSourceStatusEventArgs e )
        {
            if (e.ReturnValue == null || ((ArtGalleryDS.PictureDataTable)e.ReturnValue).Rows.Count == 0)
                Response.Redirect( "~/default.aspx", true );
        }
   }
}