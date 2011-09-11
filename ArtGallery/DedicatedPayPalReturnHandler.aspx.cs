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
        protected void Page_Load( object sender, EventArgs e )
        {

        }

        protected void btnBuy_PayPal_Returned( object sender, SpiceLogic.PayPalCtrlForWPS.DedicatedPayPalReturnedEventArgs e )
        {
            if (e.IsPaymentCancelled)
            {
                MultiViewMyShop.SetActiveView( ViewPaymentCancelled );
                if (e.AdditionalDataItems["unframed"] == "true")
                {
                    pnlCancelledFramed.Visible = true;
                }
                else
                    pnlCancelUnframed.Visible = true;

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