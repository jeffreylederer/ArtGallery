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
            btnBuy.Weight = (row["weight"] == DBNull.Value ? (decimal)0.0 : row.weight) + (row["packingweight"] == DBNull.Value ? (decimal)0.0 : row.packingweight);
            btnBuy.ItemNumber = string.Format( "{0:0}:{1:0}", row.pictureid, id );
            if (row["handling"] != DBNull.Value)
                btnBuy.Handling = row.handling;
            btnBuy.Amount = row.price;

            ArtGalleryDS.PayPalRow row1 = ArtGallery.PayPayDL.GetAcive();
            if (row1 == null)
                return false;

            btnBuy.PayPalFormSubmission.PostDestination = row1.mode == "sandbox" ? FormSubmissionSettings.PostActionDestinations.PayPal_Sandbox :
                FormSubmissionSettings.PostActionDestinations.PayPal_Website;
            btnBuy.PayPalFormSubmission.PostActionUrl = row1.buynowurl;
            btnBuy.Submit();
            return true;
        }

        protected bool GenerateInvoice( int id, SpiceLogic.PayPalCtrlForWPS.Controls.BuyNowButton btnBuy )
        {
            ArtGalleryDS.PictureDataTable table = PictureDL.GetById( id );
            if (table == null || table.Rows.Count != 1)
                return false;
            ArtGalleryDS.PictureRow row = table[0];
            btnBuy.ItemName = row.Title;
            btnBuy.ItemNumber = id.ToString();
            btnBuy.Weight = (decimal)(row["weight"] == DBNull.Value ? 0.0 : row.weight) + (row["Packingweight"] == DBNull.Value ? (decimal)0.0 : row.Packingweight);
            btnBuy.ItemNumber = string.Format( "{0:0}:{1:0}", id, row.id );
            if(row["handling"] != DBNull.Value)
                btnBuy.Handling = row.Handling;
            btnBuy.Amount = row.price;

            ArtGalleryDS.PayPalRow row1 = ArtGallery.PayPayDL.GetAcive();
            if (row1 == null)
                return false;

            btnBuy.PayPalFormSubmission.PostDestination = row1.mode == "sandbox" ? FormSubmissionSettings.PostActionDestinations.PayPal_Sandbox :
                FormSubmissionSettings.PostActionDestinations.PayPal_Website;
            btnBuy.PayPalFormSubmission.PostActionUrl = row1.buynowurl;
            btnBuy.Submit();
            return true;
        }

        protected void BuyNowButton_PayPal_Returned( object sender, BuyNowReturnedEventArgs e )
        {
            if (e.IsPaymentCancelled)
            {

            }
            else
            {


                switch (e.PDT.Status)
                {
                    case PayPalPDT.StatusCodes.SUCCESS:
                        break;
                    case PayPalPDT.StatusCodes.NOT_Handling_PDT:
                        break;
                }
            }
        }
    }


}