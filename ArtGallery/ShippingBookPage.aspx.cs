using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtGallery.DataLayer;
using SpiceLogic.PayPalCtrlForWPS.Core;
using RateWSSample.RateWebReference;

namespace ArtGallery
{
    public partial class ShippingBookPage : System.Web.UI.Page
    {

        protected void Page_PreRender( object sender, EventArgs e )
        {
            if (!IsPostBack)
            {
                try
                {
                    int id = int.Parse( Request.QueryString["id"] );
                    ArtGalleryDS.BookDataTable table = BookDL.GetById( id );
                    lblTitle.Text = table[0].title;
                    lblDescription.Text = table[0].description;

                }
                catch
                {
                    Response.Redirect( "default.aspx" );
                }
            }
        }

        protected void btnBuy_Click( object sender, ImageClickEventArgs e )
        {

             int id = int.Parse( Request.QueryString["id"] );
             ArtGalleryDS.BookDataTable table = BookDL.GetById(id);
             if (table == null || table.Rows.Count != 1)
             {
                 return;
             }
             ArtGalleryDS.BookRow row = table[0];
            

            

            ArtGalleryDS.PayPalRow row1 = PayPayDL.GetAcive();
            if (row1 == null)
            {
                return;
            }

            #region setup the button
            btnBuy.GenerateEncryptedButton = true;
            btnBuy.BusinessEmailOrMerchantID = row1.BusinessEmailOrMerchantID;

            // EncryptedButtonGeneration
            btnBuy.EncryptedButtonGeneration.PayPalCertPath = "~/App_Data/" + row1.PayPalCertPath;
            btnBuy.EncryptedButtonGeneration.PKCS12CertPath = "~/App_Data/" + row1.PKCS12CertFile;
            btnBuy.EncryptedButtonGeneration.PKCS12Password = row1.PKCS12Password;
            btnBuy.EncryptedButtonGeneration.CertificateId = row1.CertificateId;

            // PayPalReturn
            btnBuy.PayPalReturn.Custom_CancelledReturnURL = "~/DedicatedPayPalReturnHandler.aspx?sLPPCStatus=cancel";
            btnBuy.PayPalReturn.Custom_CompletedReturnURL = "~/DedicatedPayPalReturnHandler.aspx";
            btnBuy.PayPalReturn.PDTAuthenticationToken = row1.PDTAuthenticationToken;

            //paypalformsubmission
            if (row1.mode == "sandbox")
            {
                btnBuy.PayPalFormSubmission.PostDestination = FormSubmissionSettings.PostActionDestinations.PayPal_Sandbox;
                btnBuy.PayPalFormSubmission.PostActionUrl = "https://www.sandbox.paypal.com/cgi-bin/webscr";
            }
            else
            {
                btnBuy.PayPalFormSubmission.PostDestination = FormSubmissionSettings.PostActionDestinations.PayPal_Website;
                btnBuy.PayPalFormSubmission.PostActionUrl = "https://www.paypal.com/cgi-bin/webscr";
            }

            #endregion

            
            btnBuy.Amount = row.price;
            if(ddlShipping.SelectedValue != "00")
                btnBuy.Handling = (decimal) 3.0;
            btnBuy.ItemName = "Book: " + row.title;
            btnBuy.ItemNumber = id.ToString();
            btnBuy.BuyerInfo.FirstName = txtFirstName.Text;
            btnBuy.BuyerInfo.LastName = txtLastName.Text;
            btnBuy.BuyerInfo.StAddress1 = txtAddress.Text;
            btnBuy.BuyerInfo.StAddress2 = txtAddress1.Text;
            btnBuy.BuyerInfo.City = txtCity.Text;
            btnBuy.BuyerInfo.State = ddlState.SelectedValue;
            btnBuy.BuyerInfo.Zip = txtZipCode.Text;
            btnBuy.BuyerInfo.EmailAddress = txtEmail.Text;
            btnBuy.BuyerInfo.Country = ddlCountry.SelectedValue;
            btnBuy.Shipping = ShippingCost( row );
            decimal salesTax = 0;
            if (ddlShipping.SelectedValue == "00")
                salesTax = SalesTaxDL.Get( "15217" );  // change to ship from address
            else if(ddlCountry.SelectedValue == "US")
                salesTax = SalesTaxDL.Get( txtZipCode.Text );
            if (salesTax > 0)
                btnBuy.Tax = salesTax*btnBuy.Amount;
            btnBuy.AdditionalDataItems["shipping method"] = ddlShipping.SelectedItem.Text;
            btnBuy.AdditionalDataItems["mechandize"] = "book";
            pnlForm.Visible = false;
            pnlProcessing.Visible = true;
            btnBuy.ImageUrl = "Images/dot.jpg";


        }

        private bool GenerateInvoice(int id, ArtGalleryDS.BookRow row)
        {


            
            
            return true;
        }

       

        

        protected void ddlCountry_SelectedIndexChanged( object sender, EventArgs e )
        {
            
            ddlState.Items.Clear();
            ddlState.Items.Add( new ListItem() );
            ddlState.DataBind();
            if (ddlCountry.SelectedValue == "CA")
            {
                lblZip.Text = "Postal Code";
                reZip.ValidationExpression = @"[A-Za-z]\d[A-Za-z]\d[A-Za-z]\d";
                rqZip.ErrorMessage = "Postal Code is required";
                reZip.ErrorMessage = "Not acceptable Postal Code";

                reZipCalc.ValidationExpression = @"[A-Za-z]\d[A-Za-z]\d[A-Za-z]\d";
                reZipCalc.ErrorMessage = "Not acceptable Postal Code";
                RqZipCalc.ErrorMessage = "Postal Code is required";

                ddlShipping.Items.Clear();
                ddlShipping.Items.Add( new ListItem( "Local Pickup", "00" ) );
                ddlShipping.Items.Add( new ListItem( "Standard", "11" ) );
                ddlShipping.Items.Add( new ListItem( "Saver", "65" ) );
                ddlShipping.SelectedIndex = 2;

            }
            else
            {
                lblZip.Text = "Zipcode";
                reZip.ValidationExpression=@"\d{5}(-\d{4})?";
                reZip.ErrorMessage = "Not acceptable Zipcode";
                rqZip.ErrorMessage = "Zipcode is required";

                reZipCalc.ValidationExpression = @"\d{5}(-\d{4})?";
                reZipCalc.ErrorMessage = "Not acceptable Zipcode";
                RqZipCalc.ErrorMessage = "Zipcode is required";

                ddlShipping.Items.Clear();
                ddlShipping.Items.Add( new ListItem( "Local Pickup", "00" ) );
                ddlShipping.Items.Add( new ListItem( "Media Mail", "10" ) );
                ddlShipping.Items.Add( new ListItem( "Priority Mail", "20"));
                ddlShipping.Items.Add( new ListItem( "UPS Next Day Air", "01" ) );
                ddlShipping.Items.Add( new ListItem( "UPS Second Day Air", "02" ) );
                ddlShipping.Items.Add( new ListItem( "UPS Ground", "03" ) );
                ddlShipping.SelectedIndex = 1;


            }
            up1.Update();
        }

        private decimal ShippingCost(ArtGalleryDS.BookRow row )
        {
            if (ddlShipping.SelectedValue == "00")
                return 0;
            else if (ddlShipping.SelectedValue == "10")
            {
                int wght = (int) Math.Ceiling((double) row.weight);
                if(wght==1)
                    return (decimal) 2.69;
                else
                    return (decimal)(2.69 + (wght - 1)*.8);
                
            }
            else if (ddlShipping.SelectedValue == "20")
                return (decimal) 8.0;
            try
            {
                RateService rate = new RateService();
                RateRequest rateRequest = new RateRequest();
                rate.Url = "https://wwwcie.ups.com/webservices/Rate";

                // setup the command to be called
                RequestType request = new RequestType();
                String[] requestOption = { "Rate" };
                request.RequestOption = requestOption;
                rateRequest.Request = request;

                #region security
                UPSSecurity upss = new UPSSecurity();
                UPSSecurityServiceAccessToken upssSvcAccessToken = new UPSSecurityServiceAccessToken();
                upssSvcAccessToken.AccessLicenseNumber = "0CD99FA7BB48C0C2";
                upss.ServiceAccessToken = upssSvcAccessToken;
                UPSSecurityUsernameToken upssUsrNameToken = new UPSSecurityUsernameToken();
                upssUsrNameToken.Username = "jlederer17";
                upssUsrNameToken.Password = "Ilene913";
                upss.UsernameToken = upssUsrNameToken;
                rate.UPSSecurityValue = upss;
                #endregion

                #region shipment information
                ShipmentType shipment = new ShipmentType();

                // shipper information
                ShipperType shipper = new ShipperType();
                shipper.ShipperNumber = "01337R";
                AddressType shipperAddress = new AddressType();
                String[] addressLine = { "986 Lilac St"};
                shipperAddress.AddressLine = addressLine;
                shipperAddress.City = "Pittsburgh";
                shipperAddress.PostalCode = "15217";
                shipperAddress.StateProvinceCode = "PA";
                shipperAddress.CountryCode = "US";
                shipperAddress.AddressLine = addressLine;
                shipper.Address = shipperAddress;
                shipment.Shipper = shipper;

                // ship from information
                ShipFromType shipFrom = new ShipFromType();
                shipFrom.Address = shipperAddress;
                shipment.ShipFrom = shipFrom;

                // ship to information
                ShipToType shipTo = new ShipToType();
                ShipToAddressType shipToAddress = new ShipToAddressType();

                shipTo.Name = txtFirstName.Text + " " + txtLastName.Text;
                if (string.IsNullOrWhiteSpace( txtAddress1.Text ))
                {
                    String[] addressLine1 = new string[1];
                    addressLine1[0] = txtAddress.Text;
                    shipToAddress.AddressLine = addressLine1;
                }
                else
                {
                    String[] addressLine1 = new string[2];
                    addressLine1[0] = txtAddress.Text;
                    addressLine1[1] = txtAddress1.Text;
                    shipToAddress.AddressLine = addressLine1;
                }
                shipToAddress.City = txtCity.Text;
                shipToAddress.PostalCode = txtZipCode.Text;
                shipToAddress.StateProvinceCode = ddlState.SelectedValue;
                shipToAddress.CountryCode = ddlCountry.SelectedValue;
                shipToAddress.ResidentialAddressIndicator = "True";  // make this a flag on the page
                shipTo.Address = shipToAddress;
                shipment.ShipTo = shipTo;

                // shipping method
                CodeDescriptionType service = new CodeDescriptionType();
                service.Code = ddlShipping.SelectedValue;
                service.Description = ddlShipping.SelectedItem.Text;
                shipment.Service = service;

                // package weight information
                PackageType package = new PackageType();
                PackageWeightType packageWeight = new PackageWeightType();
                CodeDescriptionType uom = new CodeDescriptionType();
                uom.Code = "LBS";
                uom.Description = "pounds";
                packageWeight.UnitOfMeasurement = uom;
                packageWeight.Weight = Math.Ceiling( row.weight ).ToString( "#" );
                package.PackageWeight = packageWeight;

                // package size
                CodeDescriptionType uomDimension = new CodeDescriptionType();
                DimensionsType dimensionType = new DimensionsType();
                dimensionType.Height = Math.Ceiling(row.height).ToString("#");
                dimensionType.Width = "1";
                dimensionType.Length = Math.Ceiling(row.width).ToString("#");
                uomDimension.Code = "IN";
                dimensionType.UnitOfMeasurement = uomDimension;
                package.Dimensions = dimensionType;

                // how package is packed
                CodeDescriptionType packType = new CodeDescriptionType();
                packType.Code = "02";  //customer supplied package
                package.PackagingType = packType;
                PackageType[] pkgArray = { package };
                shipment.Package = pkgArray;

                // cost of package for insurance
                InvoiceLineTotalType invoice = new InvoiceLineTotalType();
                invoice.CurrencyCode = "USD";
                invoice.MonetaryValue = row.price.ToString( "#" );
                shipment.InvoiceLineTotal = invoice;

                rateRequest.Shipment = shipment;
                #endregion

                // how UPS gets package
                CodeDescriptionType pickupType = new CodeDescriptionType();
                pickupType.Code = "06"; //one time pickup
                rateRequest.PickupType = pickupType;

                // what type of customer
                CodeDescriptionType customerClassfication = new CodeDescriptionType();
                customerClassfication.Code = "04";  // Retail rate
                rateRequest.CustomerClassification = customerClassfication;

                System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();

                //call web service
                RateResponse rateResponse =rate.ProcessRate(rateRequest);
                string cost = rateResponse.RatedShipment[0].TotalCharges.MonetaryValue;
                return decimal.Parse( cost );
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                ErrorLogDL.Insert( ex );
                string mess1="SoapException Category:Code:Message= " + ex.Detail.LastChild.InnerText;
                string mess2 = "SoapException XML String for all= " + ex.Detail.LastChild.OuterXml;
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                ErrorLogDL.Insert( ex );

            }
            catch (Exception ex)
            {
                ErrorLogDL.Insert( ex );

            }
            return decimal.Parse("0");
        }

        protected void btnCalculate_Click( object sender, EventArgs e )
        {
            int id = int.Parse( Request.QueryString["id"] );
            BookInfo bookInfo = new BookInfo();
            ArtGalleryDS.BookRow row = BookDL.GetById( id )[0];
            
            
            bookInfo.handling = 3;
            if(ddlShipping.SelectedValue == "00")
            {
                bookInfo.tax = SalesTaxDL.Get( "15217" ) * row.price; // needs to be changed shipfrom address
                bookInfo.handling=0;
            }
            else if (ddlCountry.SelectedValue == "US")
                bookInfo.tax = SalesTaxDL.Get(txtZipCode.Text) * row.price;
            else
                bookInfo.tax = 0;
            bookInfo.price = row.price;
            bookInfo.shipping = ShippingCost(row);
            bookInfo.total = row.price + bookInfo.handling + bookInfo.shipping + bookInfo.tax;
            BookInfo[] list = { bookInfo };
            fv.DataSource = list;
            fv.DataBind();
            up2.Update();
            
        }
    }

    internal class BookInfo
    {
        
        public decimal price { get; set; }
        public decimal tax { get; set; }
        public decimal total { get; set; }
        public decimal shipping { get; set; }
        public decimal handling { get; set; }
      

        public BookInfo()
        {
            handling = 0;
            shipping = 0;

        }

    }
}