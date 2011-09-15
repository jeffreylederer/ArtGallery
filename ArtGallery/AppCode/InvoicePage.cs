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

        protected bool GenerateInvoiceReproduction( int id, SpiceLogic.PayPalCtrlForWPS.Controls.BuyNowButton btnBuy )
        {
            ArtGalleryDS.ReproductionDataTable table = ReproductionDL.GetById( id );
            if (table == null || table.Rows.Count != 1)
                return false;
            ArtGalleryDS.ReproductionRow row = table[0];
            btnBuy.ItemName = string.Format("{0}: Print {1:0.00} by {2:0.00}", row.Title, row.height, row.width);
            btnBuy.ItemNumber = row.pictureid.ToString();
            btnBuy.Weight = 1;
            btnBuy.ItemNumber = string.Format( "{0:0}:{1:0}", row.pictureid, id );
            btnBuy.Handling = 10;
            btnBuy.Amount = row.price;
            btnBuy.AdditionalDataItems["unframed"] = "true";
            btnBuy.AdditionalDataItems["width"] = row.width.ToString();
            btnBuy.AdditionalDataItems["height"] = row.height.ToString();
            btnBuy.AdditionalDataItems["strongbox"] = "none needed";


            ArtGalleryDS.PayPalRow row1 = ArtGallery.PayPayDL.GetAcive();
            if (row1 == null)
                return false;

            btnBuy.PayPalFormSubmission.PostDestination = row1.mode == "sandbox" ? FormSubmissionSettings.PostActionDestinations.PayPal_Sandbox :
                FormSubmissionSettings.PostActionDestinations.PayPal_Website;
            btnBuy.PayPalFormSubmission.PostActionUrl = row1.buynowurl;
            btnBuy.Submit();
            return true;
        }

        protected bool GenerateInvoice( int id, SpiceLogic.PayPalCtrlForWPS.Controls.BuyNowButton btnBuy, bool unframe )
        {
            ArtGalleryDS.PictureDataTable table = PictureDL.GetById( id );
            if (table == null || table.Rows.Count != 1)
                return false;
            ArtGalleryDS.PictureRow row = table[0];
            btnBuy.ItemName = row.Title + (unframe ? " (unframed)" : "");
            btnBuy.ItemNumber = id.ToString();
            if (!unframe)
            {
                Calculate( btnBuy, row.Width, row.Height, row.Frame, row.weight );
            }
            else
            {
                btnBuy.Handling = 10;
                btnBuy.Weight = (decimal) 2.0;
                btnBuy.AdditionalDataItems["strongbox"] = "none needed";
            }
            btnBuy.ItemNumber = id.ToString();
            btnBuy.AdditionalDataItems["unframed"] = unframe.ToString();
            btnBuy.AdditionalDataItems["width"] = "0";
            btnBuy.AdditionalDataItems["height"] = "0";
                 
             
            btnBuy.Amount = (decimal) row.price;

            ArtGalleryDS.PayPalRow row1 = ArtGallery.PayPayDL.GetAcive();
            if (row1 == null)
                return false;

            btnBuy.PayPalFormSubmission.PostDestination = row1.mode == "sandbox" ? FormSubmissionSettings.PostActionDestinations.PayPal_Sandbox :
                FormSubmissionSettings.PostActionDestinations.PayPal_Website;
            btnBuy.PayPalFormSubmission.PostActionUrl = row1.buynowurl;
            btnBuy.Submit();
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
                }
            }


        }
    }


}