using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtGallery.DataLayer;
using System.Net;
using System.Text;
using SpiceLogic.PayPalCtrlForWPS;
using SpiceLogic.PayPalCtrlForWPS.Core;
using System.Data;

namespace ArtGallery
{

    public partial class InvoicePage : Page
    {

        protected void SetupButton( SpiceLogic.PayPalCtrlForWPS.Controls.BuyNowButton btnBuy )
        {
            //ArtGalleryDS.PayPalRow row1 = PayPayDL.GetAcive();
            //if (row1 == null)
            //{
            //    return;
            //}
            //btnBuy.PayPalReturn.Custom_CancelledReturnURL = "~/DedicatedPayPalReturnHandler.aspx?sLPPCStatus=cancel";
            //btnBuy.PayPalReturn.Custom_CompletedReturnURL = "~/DedicatedPayPalReturnHandler.aspx";
            //btnBuy.PayPalReturn.PDTAuthenticationToken = row1.PDTAuthenticationToken;

            //btnBuy.EncryptedButtonGeneration.PayPalCertPath = "~/App_Data/" + row1.PayPalCertPath;
            //btnBuy.EncryptedButtonGeneration.PKCS12CertPath = "~/App.Data/" + row1.PKCS12CertFile;
            //btnBuy.EncryptedButtonGeneration.PKCS12Password = row1.PKCS12Password;
            //btnBuy.EncryptedButtonGeneration.CertificateId = row1.CertificateId;

            //btnBuy.PayPalFormSubmission.PostDestination = row1.mode == "sandbox" ? FormSubmissionSettings.PostActionDestinations.PayPal_Sandbox :
            //    FormSubmissionSettings.PostActionDestinations.PayPal_Website;
            //btnBuy.PayPalFormSubmission.PostActionUrl = row1.buynowurl;
        }

        protected bool GenerateInvoiceReproduction( int id, SpiceLogic.PayPalCtrlForWPS.Controls.BuyNowButton btnBuy )
        {
            var table = ReproductionDL.GetById( id );
            if (table == null || table.Rows.Count != 1)
                return false;
            var row = table[0];
            btnBuy.ItemName = string.Format("{0}: Print {1:0.00} by {2:0.00}", row.Title, row.height, row.width);
            btnBuy.ItemNumber = row.pictureid.ToString();
            btnBuy.Weight = 1;
            btnBuy.ItemNumber = string.Format( "{0:0}:{1:0}", row.pictureid, id );
            btnBuy.Handling = 10;
            btnBuy.Amount = row.price;
            btnBuy.Height = (int) Math.Min( row.width, row.height );
            btnBuy.Width = 3;
            btnBuy.AdditionalDataItems["unframed"] = "true";
            btnBuy.AdditionalDataItems["width"] = row.width.ToString();
            btnBuy.AdditionalDataItems["height"] = row.height.ToString();
            btnBuy.AdditionalDataItems["strongbox"] = "none needed";
                       
            return true;
        }

        protected bool GenerateInvoice( int id, SpiceLogic.PayPalCtrlForWPS.Controls.BuyNowButton btnBuy, bool unframe )
        {
            var table = PictureDL.GetById( id );
            if (table == null || table.Rows.Count != 1)
                return false;
            var row = table[0];
            btnBuy.ItemName = row.Title + (unframe ? " (unframed)" : "");
            btnBuy.ItemNumber = id.ToString();
            if (!unframe)
            {
                Calculate( btnBuy, row.Width, row.Height, row.Frame, row.weight );
            }
            else
            {
                btnBuy.Height = (int)Math.Ceiling( Math.Min( row.Width, row.Height ) );
                btnBuy.Width = 3;
                btnBuy.Handling = 10;
                btnBuy.Weight = (decimal) 2.0;
                btnBuy.AdditionalDataItems["strongbox"] = "none needed";
            }
            btnBuy.ItemNumber = id.ToString();
            btnBuy.AdditionalDataItems["unframed"] = unframe.ToString().ToLower();
            btnBuy.AdditionalDataItems["width"] = "0";
            btnBuy.AdditionalDataItems["height"] = "0";
            btnBuy.Amount = (decimal) row.price;

            return true;
        }

  

        private void  Calculate( SpiceLogic.PayPalCtrlForWPS.Controls.BuyNowButton btnBuy, double width, double length, string frame, decimal weight )
        {
            ArtGalleryDS.StrongBoxDataTable table = StrongBoxDL.Get();
            double big = Math.Max( width, length );
            double small = Math.Min( width, length );

            if(frame.ToUpper().Contains("GLASS"))
            {
                var query = from p in table where p.length >= big && p.width >= small &&  p.doublewalled  orderby p.price select p;
                if(query.Count() == 0)
                {
                    var query1 = from p in table where p.doublewalled orderby p.price descending select p;
                    btnBuy.Weight = (decimal) weight + query1.First().weight;
                    btnBuy.Handling = query1.First().price;
                    btnBuy.AdditionalDataItems["strongbox"] = "too big - glass";
                }
                else
                {
                    btnBuy.Weight = (decimal) weight + query.First().weight;
                    btnBuy.Handling = query.First().price;
                    btnBuy.AdditionalDataItems["strongbox"] = query.First().modelno;
                    btnBuy.Height = query.First().height;
                    btnBuy.Width = query.First().width;
                }
            }
            else
            {
                var query = from p in table where p.length >= big && p.width >= small && !p.doublewalled orderby p.price select p;
                if (query.Count() == 0)
                {
                    var query1 = from p in table where !p.doublewalled orderby p.price descending select p;
                    btnBuy.Weight = (decimal)weight + query1.First().weight;
                    btnBuy.Handling = query1.First().price;
                    btnBuy.AdditionalDataItems["strongbox"] = "too big - plexi";
                }
                else
                {
                    btnBuy.Weight = (decimal)weight + query.First().weight;
                    btnBuy.Handling = query.First().price;
                    btnBuy.AdditionalDataItems["strongbox"] = query.First().modelno;
                    btnBuy.Height = query.First().height;
                    btnBuy.Width = query.First().width;
                }
            }


        }
    }


}