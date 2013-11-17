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
    public partial class PayPal : System.Web.UI.Page
    {
       
        /// <summary>
        /// When user selects a different PayPal mode, this event method rebinds the Form View and
        /// updates the panel the Form View is inside.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void modeDropDownList_SelectedIndexChanged( object sender, EventArgs e )
        {
            fmPayPal.DataBind();
            upPayPal.Update();
        }

        /// <summary>
        /// Event when user select upload Get PayPal Cert File button. It uploads the  file to the
        /// App_Data directory and writes the file name in the Get PayPal Cert File Name text box and updates
        /// that column in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UploadPayPalButton_Click( object sender, EventArgs e )
        {
            //FileUpload FileUpPayPal = fmPayPal.FindControl( "FileUpPayPal" ) as FileUpload;
            if (FileUpPayPal.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName( FileUpPayPal.FileName );
                    FileUpPayPal.SaveAs( Server.MapPath( "~/App_Data/" ) + filename );
                    ErrorLabelPayPal.Text = "Upload status: PayPal File uploaded!";
                    PayPayDL.UpdatePayPalPath( filename, modeDropDownList.SelectedValue );
                    fmPayPal.DataBind();
                }
                catch (Exception ex)
                {
                    ErrorLabelPayPal.Text = "Upload status: The file could not be uploaded. The following error occurred: " + ex.Message;
                }
            }
            else
                ErrorLabelPayPal.Text = "Could not find file";
            upPayPal.Update();
        }

        /// <summary>
        /// Event when user select upload PKCS12 Cert File button. It uploads the  file to the
        /// App_Data directory and writes the file name in the PKCS12 Cert File Name text box and updates
        /// that column in the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UploadPKCS12CertButton_Click( object sender, EventArgs e )
        {
            //FileUpload FileUplPKCS12Cert = fmPayPal.FindControl( "FileUplPKCS12Cert" ) as FileUpload;
            if (FileUplPKCS12Cert.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName( FileUplPKCS12Cert.FileName );
                    FileUplPKCS12Cert.SaveAs( Server.MapPath( "~/App_Data/" ) + filename );
                    ErrorLabelPayPal.Text = "Upload status: PKCS12 Cert File uploaded!";
                    PayPayDL.UpdatePKCS12CertPath( filename, modeDropDownList.SelectedValue );
                    fmPayPal.DataBind();
                }
                catch (Exception ex)
                {
                    ErrorLabelPayPal.Text = "Upload status: The file could not be uploaded. The following error occurred: " + ex.Message;
                }
            }
            else
                ErrorLabelPayPal.Text = "Could not find file";
            upPayPal.Update();
        }
    }
}