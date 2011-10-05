﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtGallery.DataLayer;
using SpiceLogic.PayPalCtrlForWPS;
using SpiceLogic.PayPalCtrlForWPS.Core;
using RateWSSample.RateWebReference;
using System.ServiceModel;
using System.Net.Security;

namespace ArtGallery
{
    public partial class ShippingPage : System.Web.UI.Page
    {

        protected void Page_PreRender( object sender, EventArgs e )
        {
            if (!IsPostBack)
            {
                try
                {
                    int id = int.Parse( Request.QueryString["id"] );
                    bool repro = bool.Parse( Request.QueryString["repro"] );
                    if (repro)
                    {
                        cellUnframed.Visible = false;
                    }
                    else
                    {
                        ArtGalleryDS.PictureDataTable table = PictureDL.GetById( id );
                        if (table == null || table.Rows.Count != 1)
                        {
                            cellUnframed.Visible = false;
                        }
                        else
                            cellUnframed.Visible = !table[0].Frame.ToUpper().Contains( "UNFRAMED" );
                    }
                    ArtGalleryDS.Picture_GetInfoDataTable ptable = PictureDL.GetInfo( id, repro );
                    if (ptable != null && ptable.Rows.Count == 1)
                    {
                        lblTitle.Text = ptable[0].title;
                        lblDescription.Text = ptable[0].description;
                    }


                }
                catch
                {
                    Response.Redirect( "default.aspx" );
                }
            }
        }

        protected void btnBuy_Click( object sender, ImageClickEventArgs e )
        {
            int id = int.Parse(Request.QueryString["id"]);
            bool repro = bool.Parse(Request.QueryString["repro"]);

            PictureInfo pictureInfo = new PictureInfo();

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

            if (repro)
            {
                if (!GenerateInvoiceReproduction( id, pictureInfo))
                {
                    btnBuy.CancelSubmission();
                    return;
                }
            }
            else
            {
                if (!GenerateInvoice( id, chkUnframed.Checked, pictureInfo ))
                {
                    btnBuy.CancelSubmission();
                    return;
                }
            }

            btnBuy.Amount = pictureInfo.amount;
            btnBuy.Handling = pictureInfo.handling;

            btnBuy.BuyerInfo.FirstName = txtFirstName.Text;
            btnBuy.BuyerInfo.LastName = txtLastName.Text;
            btnBuy.BuyerInfo.StAddress1 = txtAddress.Text;
            btnBuy.BuyerInfo.StAddress2 = txtAddress1.Text;
            btnBuy.BuyerInfo.City = txtCity.Text;
            btnBuy.BuyerInfo.State = ddlState.SelectedValue;
            btnBuy.BuyerInfo.Zip = txtZipCode.Text;
            btnBuy.BuyerInfo.EmailAddress = txtEmail.Text;
            btnBuy.BuyerInfo.Country = ddlCountry.SelectedValue;
            btnBuy.Shipping = ShippingCost( pictureInfo );
            decimal salesTax = 0;
            if (ddlShipping.SelectedValue == "00")
                salesTax = SalesTaxDL.Get( "15217" );  // change to ship from address
            if(ddlCountry.SelectedValue == "US")
                salesTax = SalesTaxDL.Get( txtZipCode.Text );
            if (salesTax > 0)
                btnBuy.Tax = salesTax*btnBuy.Amount;
            btnBuy.AdditionalDataItems["shipping method"] = ddlShipping.SelectedItem.Text;
            pnlForm.Visible = false;
            pnlProcessing.Visible = true;
            btnBuy.ImageUrl = "Images/dot.jpg";


        }

        private bool GenerateInvoice( int id, bool unframe, PictureInfo pictureInfo )
        {
            
            ArtGalleryDS.PictureDataTable table = PictureDL.GetById( id );
            if (table == null || table.Rows.Count != 1)
                return false;
            ArtGalleryDS.PictureRow row = table[0];
            btnBuy.ItemName = row.Title + (unframe ? " (unframed)" : "");
            btnBuy.ItemNumber = id.ToString();
            if (ddlShipping.SelectedValue != "00")
            {
                if (!unframe)
                {
                    Calculate( row.Width, row.Height, row.Frame, row.weight, pictureInfo );
                }
                else
                {
                    pictureInfo.handling = 10;
                    pictureInfo.packagingWeight = 1.5m;
                    pictureInfo.pictureWeight = .5m;
                    pictureInfo.height = (decimal)Math.Ceiling( Math.Max( row.Width, row.Height ) );
                    pictureInfo.width = 3;
                    pictureInfo.tall = 3;
                    btnBuy.AdditionalDataItems["strongbox"] = "none needed";

                }
            }
            pictureInfo.amount = (decimal)row.price;
            btnBuy.ItemNumber = id.ToString();
            btnBuy.AdditionalDataItems["unframed"] = unframe.ToString().ToLower();
            btnBuy.AdditionalDataItems["width"] = table[0].Width.ToString();
            btnBuy.AdditionalDataItems["height"] = table[0].Height.ToString();
            return true;
        }

        private bool GenerateInvoiceReproduction( int id, PictureInfo pictureInfo)
        {
            
            ArtGalleryDS.ReproductionDataTable table = ReproductionDL.GetById( id );
            if (table == null || table.Rows.Count != 1)
                return false;
            ArtGalleryDS.ReproductionRow row = table[0];

            ArtGalleryDS.PictureDataTable table1 = PictureDL.GetById( table[0].pictureid );
            if (table1 == null || table1.Rows.Count != 1)
                return false;

            btnBuy.ItemName = string.Format("{0}: Print {1:0.00} by {2:0.00}", row.Title, row.height, row.width);
            btnBuy.ItemNumber = row.pictureid.ToString();

            if (ddlShipping.SelectedValue != "00")
            {
                pictureInfo.handling = 10;
                pictureInfo.packagingWeight = 1.5m;
                pictureInfo.pictureWeight = .5m;
                pictureInfo.amount = row.price;
                pictureInfo.height = (decimal)Math.Ceiling( Math.Min( row.width, row.height ) );
                pictureInfo.width = 3;
                pictureInfo.tall = 3;
            }
            pictureInfo.amount = row.price;


            btnBuy.AdditionalDataItems["unframed"] = "true";
            btnBuy.AdditionalDataItems["width"] = row.width.ToString();
            btnBuy.AdditionalDataItems["height"] = row.height.ToString();
            btnBuy.AdditionalDataItems["strongbox"] = "none needed";
                       
            return true;
        }

        private decimal Calculate( double width, double length, string frame, decimal weight, PictureInfo pictureInfo )
        {
            ArtGalleryDS.StrongBoxDataTable table = StrongBoxDL.Get();
            double big = Math.Max( width, length );
            double small = Math.Min( width, length );
            decimal totalweight = 0;

            if (frame.ToUpper().Contains( "GLASS" ))
            {
                var query = from p in table where p.length >= big && p.width >= small && p.doublewalled orderby p.price select p;
                if (query.Count() == 0)
                {
                    var query1 = from p in table where p.doublewalled orderby p.price descending select p;
                    pictureInfo.pictureWeight = (decimal)weight;
                    pictureInfo.packagingWeight =  query1.First().weight;
                    pictureInfo.handling = query1.First().price;
                    btnBuy.AdditionalDataItems["strongbox"] = "too big - glass";
                    pictureInfo.tall = 4;
                    pictureInfo.width = (decimal) width*.25m;
                    pictureInfo.height =(decimal) length*.25m;
                }
                else
                {
                    pictureInfo.pictureWeight = (decimal)weight;
                    pictureInfo.pictureWeight = query.First().weight;
                    pictureInfo.handling = query.First().price;
                    btnBuy.AdditionalDataItems["strongbox"] = query.First().modelno;
                    pictureInfo.width = query.First().width;
                    pictureInfo.height = query.First().length;
                    pictureInfo.tall = query.First().height;
                }
            }
            else
            {
                var query = from p in table where p.length >= big && p.width >= small && !p.doublewalled orderby p.price select p;
                if (query.Count() == 0)
                {
                    var query1 = from p in table where !p.doublewalled orderby p.price descending select p;
                    pictureInfo.pictureWeight = (decimal)weight;
                    pictureInfo.pictureWeight = query.First().weight;
                    pictureInfo.handling = query.First().price;
                    btnBuy.AdditionalDataItems["strongbox"] = "too big - plexi";
                    pictureInfo.tall = 4;
                    pictureInfo.width = (decimal)width * .25m;
                    pictureInfo.height = (decimal)length *.25m;
                }
                else
                {
                    pictureInfo.pictureWeight = (decimal)weight;
                    pictureInfo.pictureWeight = query.First().weight;
                    pictureInfo.handling = query.First().price;
                    btnBuy.AdditionalDataItems["strongbox"] = query.First().modelno;
                    pictureInfo.width = query.First().width;
                    pictureInfo.height = query.First().length;
                    pictureInfo.tall = query.First().height;
                 }
            }
            btnBuy.AdditionalDataItems["width"] = width.ToString();
            btnBuy.AdditionalDataItems["height"] = length.ToString();
            return Math.Ceiling( totalweight );
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
                ddlShipping.Items.Add( new ListItem( "Next Day Air", "01" ) );
                ddlShipping.Items.Add( new ListItem( "Second Day Air", "02" ) );
                ddlShipping.Items.Add( new ListItem( "Ground", "03" ) );
                ddlShipping.SelectedIndex = 2;


            }
            up1.Update();
        }

        private decimal ShippingCost(PictureInfo pictureInfo)
        {
            if (ddlShipping.SelectedValue == "00")
                return 0;
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
                upssSvcAccessToken.AccessLicenseNumber = "1C8755AE63604178";
                upss.ServiceAccessToken = upssSvcAccessToken;
                UPSSecurityUsernameToken upssUsrNameToken = new UPSSecurityUsernameToken();
                upssUsrNameToken.Username = "jlederer17";
                upssUsrNameToken.Password = "Jeffrey17";
                upss.UsernameToken = upssUsrNameToken;
                rate.UPSSecurityValue = upss;
                #endregion

                #region shipment information
                ShipmentType shipment = new ShipmentType();

                // shipper information
                ShipperType shipper = new ShipperType();
                shipper.ShipperNumber = "447V9A";
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
                packageWeight.Weight = Math.Ceiling( pictureInfo.pictureWeight + pictureInfo.packagingWeight ).ToString( "#" );
                package.PackageWeight = packageWeight;

                // package size
                CodeDescriptionType uomDimension = new CodeDescriptionType();
                DimensionsType dimensionType = new DimensionsType();
                dimensionType.Height = pictureInfo.tall.ToString( "#" );
                dimensionType.Width = pictureInfo.width.ToString( "#" );
                dimensionType.Length = pictureInfo.height.ToString( "#" );
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
                invoice.MonetaryValue = pictureInfo.amount.ToString( "#" );
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
            return 0;
        }

        protected void btnCalculate_Click( object sender, EventArgs e )
        {
            int id = int.Parse( Request.QueryString["id"] );
            bool repro = bool.Parse( Request.QueryString["repro"] );

            PictureInfo pictureInfo = new PictureInfo();
            if (repro)
            {
                if (!GenerateInvoiceReproduction( id, pictureInfo ))
                {
                    btnBuy.CancelSubmission();
                    return;
                }
            }
            else
            {
                if (!GenerateInvoice( id, chkUnframed.Checked, pictureInfo ))
                {
                    btnBuy.CancelSubmission();
                    return;
                }
            }
            if(ddlShipping.SelectedValue == "00")
                pictureInfo.tax = SalesTaxDL.Get( "15217" ) * pictureInfo.amount; // needs to be changed shipfrom address
            else if (ddlCountry.SelectedValue == "US")
                pictureInfo.tax = SalesTaxDL.Get( txtZipCode.Text ) * pictureInfo.amount;
            else
                pictureInfo.tax = 0;
            pictureInfo.shipping = ShippingCost(pictureInfo);
            pictureInfo.total = pictureInfo.amount + pictureInfo.handling + pictureInfo.shipping + pictureInfo.tax;
            PictureInfo[] list = { pictureInfo };
            fv.DataSource = list;
            fv.DataBind();
            up2.Update();
            
        }
    }

    internal class PictureInfo
    {
        public decimal amount { get; set; }
        public decimal height { get; set; }
        public decimal width { get; set; }
        public decimal price { get; set; }
        public decimal weight { get; set; }
        public decimal tall { get; set; }
        public decimal handling { get; set; }
        public decimal pictureWeight { get; set; }
        public decimal packagingWeight { get; set; }
        public decimal tax { get; set; }
        public decimal total { get; set; }
        public decimal shipping { get; set; }
      

        public PictureInfo()
        {
            handling = 0;
            shipping = 0;

        }

    }
}