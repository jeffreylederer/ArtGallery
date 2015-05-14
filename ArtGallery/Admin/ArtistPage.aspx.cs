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
    public partial class ArtistPage : System.Web.UI.Page
    {
        /// <summary>
        /// Event when user select upload picture button. It uploads the picture file to the
        /// App_Data directory and writes the file name for the File Name text box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UploadArtistImageButton_Click( object sender, EventArgs e )
        {
            FileUpload FileUploadArtistImage = FormView2.FindControl( "FileUploadArtistImage" ) as FileUpload;
            if (FileUploadArtistImage.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName( FileUploadArtistImage.FileName );
                    FileUploadArtistImage.SaveAs( Server.MapPath( "~/Images/" ) + filename );
                    lblErrorArtist.Text = "Artist Image Upload status: File uploaded!";
                    SiteDL.UpdateArtistImagePath( filename );
                    FormView2.DataBind();
                }
                catch (Exception ex)
                {
                    lblErrorArtist.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
            else
                lblErrorArtist.Text = "Could not find file";
            upPayPal.Update();
        }
      
    }
}