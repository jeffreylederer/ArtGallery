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
                    fmPayPal.DataBind();
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
                    fmPayPal.DataBind();
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
    }
}