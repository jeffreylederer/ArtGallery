using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ArtGallery.DataLayer;

namespace ArtGallery.Admin
{
    public partial class Setup : System.Web.UI.Page
    {
        protected void UploadLogoButton_Click( object sender, EventArgs e )
        {
            //FileUpload FileUploadLogo = FormView1.FindControl( "FileUploadLogo" ) as FileUpload;
            if (FileUploadLogo.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName( FileUploadLogo.FileName );
                    FileUploadLogo.SaveAs( Server.MapPath( "~/Images/" ) + filename );
                    ErrorLabel.Text = "Upload status: Logo File uploaded!";
                    SiteDL.UpdateLogoPath( filename );
                    FormView1.DataBind();
                }
                catch (Exception ex)
                {
                    ErrorLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
            else
                ErrorLabel.Text = "Could not find file";
            upPayPal.Update();
        }

        protected void UploadImagesButton_Click( object sender, EventArgs e )
        {
            //FileUpload FileUploadImages = FormView1.FindControl( "FileUploadImages" ) as FileUpload;
            if (FileUploadImages.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName( FileUploadImages.FileName );
                    FileUploadImages.SaveAs( Server.MapPath( "~/Images/" ) + filename );
                    ErrorLabel.Text = "Upload status: File uploaded!";
                }
                catch (Exception ex)
                {
                    ErrorLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
            else
                ErrorLabel.Text = "Could not find file";
            upPayPal.Update();
        }

        protected void odsSite_Updated( object sender, ObjectDataSourceStatusEventArgs e )
        {
            if (e.Exception == null)
            {
                ErrorLabel.Text = "Site Information Updated";
            }
        }
   }
}