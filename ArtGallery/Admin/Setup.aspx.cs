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
        protected void Page_Load( object sender, EventArgs e )
        {

        }

        #region Site
        protected void UploadLogoButton_Click( object sender, EventArgs e )
        {
            FileUpload FileUploadLogo = FormView1.FindControl( "FileUploadLogo" ) as FileUpload;
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
                ErrorLabel.Text = "Count not find file";
        }

        protected void UploadImagesButton_Click( object sender, EventArgs e )
        {
            FileUpload FileUploadImages = FormView1.FindControl( "FileUploadImages" ) as FileUpload;
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
        }

        protected void odsSite_Updated( object sender, ObjectDataSourceStatusEventArgs e )
        {
            if (e.Exception != null)
            {
                Exception ex = e.Exception;
                while (ex != null)
                {
                    ErrorLabel.Text = e.Exception.Message;
                    ex = ex.InnerException;
                }
                e.ExceptionHandled = true;
                return;
            }
            if ((int)e.ReturnValue != 1)
            {
                ErrorLabel.Text = "Not Updated";
            }
        }
        #endregion

        #region Artist
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
        }

         protected void odsArtist_Updated( object sender, ObjectDataSourceStatusEventArgs e )
        {
            if (e.Exception != null)
            {
                Exception ex = e.Exception;
                while (ex != null)
                {
                    lblErrorArtist.Text = e.Exception.Message;
                    ex = ex.InnerException;
                }
                e.ExceptionHandled = true;
                return;
            }
            if ((int)e.ReturnValue != 1)
            {
                lblErrorArtist.Text = "Not Updated";
            }
        }
        #endregion

        #region PayPal
        protected void odsPayPal_Updated(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                ErrorLabelPayPal.Text = "PayPal Record saved Successfully";
            }
         }

        protected void modeDropDownList_SelectedIndexChanged( object sender, EventArgs e )
        {
            fmPayPal.DataBind();
            upPayPal.Update();
        }
 

        protected void UploadPayPalButton_Click( object sender, EventArgs e )
        {
            FileUpload FileUpPayPal = fmPayPal.FindControl( "FileUpPayPal" ) as FileUpload;
            if (FileUpPayPal.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName( FileUpPayPal.FileName );
                    FileUpPayPal.SaveAs( Server.MapPath( "~/App_Data/" ) + filename );
                    ErrorLabelPayPal.Text = "Upload status: PayPal File uploaded!";
                    PayPayDL.UpdatePayPalPath( filename, modeDropDownList.SelectedValue );
                    FormView1.DataBind();
                }
                catch (Exception ex)
                {
                    ErrorLabelPayPal.Text = "Upload status: The file could not be uploaded. The following error occurred: " + ex.Message;
                }
            }
            else
                ErrorLabelPayPal.Text = "Count not find file";
            upPayPal.Update();
        }

        protected void UploadPKCS12CertButton_Click( object sender, EventArgs e )
        {
            FileUpload FileUplPKCS12Cert = fmPayPal.FindControl( "FileUplPKCS12Cert" ) as FileUpload;
            if (FileUplPKCS12Cert.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName( FileUplPKCS12Cert.FileName );
                    FileUplPKCS12Cert.SaveAs( Server.MapPath( "~/App_Data/" ) + filename );
                    ErrorLabelPayPal.Text = "Upload status: PKCS12 Cert File uploaded!";
                    PayPayDL.UpdatePKCS12CertPath( filename, modeDropDownList.SelectedValue );
                    FormView1.DataBind();
                }
                catch (Exception ex)
                {
                    ErrorLabelPayPal.Text = "Upload status: The file could not be uploaded. The following error occurred: " + ex.Message;
                }
            }
            else
                ErrorLabelPayPal.Text = "Count not find file";
            upPayPal.Update();
        }
        #endregion
    }
}