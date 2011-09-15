using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpiceLogic.PayPalCtrlForWPS;
using SpiceLogic.PayPalCtrlForWPS.Core;
using System.Text;

namespace ArtGallery
{
    public partial class PayPalNotification : InvoicePage
    {
       
        protected void Page_Load( object sender, EventArgs e )
        {

        }

        /// <summary>
        /// Handles the IPN_Notified event of the BuyNowButton1 control.
        /// Please NOTE: IPN URL needs to be an URL where PayPal can post data from PayPal Website.
        /// If you are testing a web application from your "LocalHost" then this Event wont fire as PayPal cannot get this
        /// page by posting notification to a URL of LocalHost (127.0.0.1 or http://localhost:xxxx/). But if you host this 
        /// page to a live server which has a real URL address then the event will be fired and this method will be executed.     
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SpiceLogic.PayPalCtrlForWPS.BuyNowIPNEventArgs"/> instance containing the event data.</param>
        protected void btnHandler_IPN_Notified( object sender, SpiceLogic.PayPalCtrlForWPS.DedicatedIPNEventArgs e )
        {
            
            if (e.IPN.Status == PayPalIPN.StatusCodes.Verified)
            {
                StringBuilder invoice = new StringBuilder();
                invoice.AppendLine( string.Format( "Transaction ID: {0}", e.TransactionID ) );
                invoice.AppendLine( string.Format( "Title: {0}", e.ItemInfo.ItemName ) );
                invoice.AppendLine( string.Format( "Item #: {0}", e.ItemInfo.ItemNumber ) );
                invoice.AppendLine( string.Format( "unframed : {0}", e.AdditionalDataItems["unframed"] ) );
                invoice.AppendLine( string.Format( "width: {0}", e.AdditionalDataItems["width"] ) );
                invoice.AppendLine( string.Format( "height: {0}", e.AdditionalDataItems["height"] ) );
                invoice.AppendLine( string.Format(" strongbox model: {0}", e.AdditionalDataItems["strongbox"]));

                invoice.AppendLine();
                invoice.AppendLine( string.Format( "Buyer : {0}", e.BuyerInfo.FirstName + " " + e.BuyerInfo.LastName ) );
                invoice.AppendLine( string.Format( "Ship To: {0}", e.BuyerInfo.ShippingAddress.AddressName ) );
                invoice.AppendLine( string.Format( "        {0}", e.BuyerInfo.ShippingAddress.Street ) );
                invoice.AppendLine( string.Format( "        {0}, {1} {2}", e.BuyerInfo.ShippingAddress.City, e.BuyerInfo.ShippingAddress.State, e.BuyerInfo.ShippingAddress.Zip ) );
                invoice.AppendLine( string.Format( "        {0}", e.BuyerInfo.ShippingAddress.Country ) );
                invoice.AppendLine( string.Format( "Email : {0}", e.BuyerInfo.PayerEmail ) );
                invoice.AppendLine( string.Format( "phone #: {0}", e.BuyerInfo.ContactPhone ) );

                invoice.AppendLine();
                invoice.AppendLine( string.Format( "Shipping : {0:0.00}", e.PaymentInfo.McShipping ) );
                invoice.AppendLine( string.Format( "Handling : {0:0.00}", e.PaymentInfo.McHandling ) );
                invoice.AppendLine( string.Format( "Tax:       {0:0.00}", e.PaymentInfo.Tax ) );
                invoice.AppendLine( string.Format( "Price:     {0:0.00}", e.PaymentInfo.McNet ) );
                invoice.AppendLine( string.Format( "Total:     {0:0.00}", e.PaymentInfo.McGross ) );
                SendMailDL.SendMail( "Sale", invoice.ToString(), false );
            }
            else
            {
                /**********************************************************************************************
                 * The price was manipulated by a FRAUD attempt. notify the admin by email about this attempt *
                 * and do not deliver the product.                                                            *
                 **********************************************************************************************/

                SendMailDL.SendMail( "Fraud Attempt detected",
                        @"Fraud attempt detected : The payment_gross value is different " +
                        "from the expected amount for the transaction id :" +
                        e.TransactionID, false );
            }
           
        }


        

        /// Handles the Exception event of the BuyNowButton1_IPN control. This event is fired if there is any Exception
        /// thrown in your IPN_Notified event handler method or if there was any exception thrown in the notification
        /// verification phase of IPN. It is better to handle this event and keep your IPN_Notified event clean without
        /// using TRY-CATCH block there.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SpiceLogic.PayPalCtrlForWPS.Core.IPNExceptionEventArgs"/> instance containing the event data.</param>
        protected void btnHandler_IPN_Exception( object sender, SpiceLogic.PayPalCtrlForWPS.Core.IPNExceptionEventArgs e )
        {
            StringBuilder debugDataBuilder = new StringBuilder();
            string errorMessage = e.IpnException.Message;
            debugDataBuilder.AppendLine( errorMessage );
            debugDataBuilder.AppendLine();
            debugDataBuilder.AppendLine( "Stack Trace : ----------" );
            debugDataBuilder.AppendLine( e.IpnException.StackTrace );
            debugDataBuilder.AppendLine();
            debugDataBuilder.AppendLine( "-----------------" );
            debugDataBuilder.AppendLine( "Exception occured at " + e.ExceptionDateTime );
            debugDataBuilder.AppendLine( "Is this ipn resent ? : " + e.IsResent );
            debugDataBuilder.AppendLine( "Sent Http Status Code : " + e.IpnResponse.StatusCode );
            SendMailDL.SendMail( "Exception", debugDataBuilder.ToString(), false );
            ErrorLogDL.Insert( e.IpnException );
        }
    }
}