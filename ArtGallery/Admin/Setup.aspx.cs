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
        /// <summary>
        /// Event when user select Upload Logo File  button. It uploads the picture file to the
        /// Images directory and writes the file name for the File Name text box and updates that column
        /// in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UploadLogoButton_Click( object sender, EventArgs e )
        {
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

        /// <summary>
        /// Event when user select upload File to Image Directory button. It uploads a file to the
        /// Images directory. These image files can be used in the home, about artist, and contact pages.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UploadImagesButton_Click( object sender, EventArgs e )
        {
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

       
   }
}