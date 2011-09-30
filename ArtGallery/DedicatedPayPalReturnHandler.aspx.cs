using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpiceLogic.PayPalCtrlForWPS;
using SpiceLogic.PayPalCtrlForWPS.Core;

namespace ArtGallery
{
    public partial class DedicatedPayPalReturnHandler : System.Web.UI.Page
    {
  
        /// <summary>
        /// This event occurs when user returns from paypal page and either the user
        /// has made a purchase or cancelled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBuy_PayPal_Returned( object sender, SpiceLogic.PayPalCtrlForWPS.DedicatedPayPalReturnedEventArgs e )
        {
            if (e.IsPaymentCancelled)
            {
                MultiViewMyShop.SetActiveView( ViewPaymentCancelled );
                if (e.AdditionalDataItems["unframed"] == "true")
                {
                    pnlCancelUnframed.Visible = true;
                }
                else
                    pnlCancelledFramed.Visible = true;

            }
            else
            {
                MultiViewMyShop.SetActiveView( ViewPaymentCompleted );

                switch (e.PDT.Status)
                {
                    case PayPalPDT.StatusCodes.SUCCESS:
                    case PayPalPDT.StatusCodes.NOT_Handling_PDT:
                        lblTitleSale.Text = e.ItemInfo.ItemName;
                        break;
                }
            }
        }
    }
}