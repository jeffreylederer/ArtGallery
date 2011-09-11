using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ArtGallery.DataLayer;
using System.Net;
using System.IO;
using System.Text;
using SpiceLogic.PayPalCtrlForWPS;
using SpiceLogic.PayPalCtrlForWPS.Core;

namespace ArtGallery
{
    public partial class PicturePage : InvoicePage
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            if (!IsPostBack)
            {
                string idStr = Request.QueryString["id"];
                if (string.IsNullOrWhiteSpace( idStr ))
                    Response.Redirect( "default.aspx" );
                int id = 0;
                if (!int.TryParse( idStr, out id ))
                    Response.Redirect( "default.aspx" );
                btnBuy.BusinessEmailOrMerchantID = (string)Application["ppAccount"];
                ProcessPicture( id );
            }
        }

        private void ProcessPicture( int id )
        {
            ArtGalleryDS.PictureDataTable table = PictureDL.GetById( id );
            FormView1.DataSource = table.Rows;
            FormView2.DataSource = table.Rows;
            FormView1.DataBind();
            FormView2.DataBind();

        }


        private void RenderPicture( int id )
        {
            Label lblSold = FormView1.FindControl( "lblSold" ) as Label;
            Label lblPrice = FormView1.FindControl( "lblPrice" ) as Label;
            Label lblPrints = FormView1.FindControl( "lblPrints" ) as Label;
            HtmlTableRow row = FormView1.FindControl( "tdPrints" ) as HtmlTableRow;
            HiddenField Available = FormView1.FindControl( "Available" ) as HiddenField;

            ArtGalleryDS.ReproductionDataTable rpt = ReproductionDL.GetByPictureId( id );
            
            
            // no originals left, but there are prints
            if(rpt.Rows.Count > 0 && Available.Value == "False")
            {
                lblPrice.Visible = false;
                btnBuy.Visible = false;
                lblPrints.Text = rpt[0].description;
   
            }
            // only originals left
            else if (Available.Value == "True" && rpt.Rows.Count == 0)
            {
                lblSold.Visible = false;
                row.Visible = false;
  
            }
            // prints and originals left
            else if (Available.Value == "True" && rpt.Rows.Count > 0)
            {
                lblSold.Visible = false;
                lblPrints.Text = rpt[0].description;
            }
            up1.Update();
            up2.Update();

            Label frame = FormView1.FindControl( "lblFrame" ) as Label;
            Panel pnlFramed = FormView1.FindControl( "pnlFramed" ) as Panel;
            CheckBox chkUnframed = FormView1.FindControl("chkUnframed") as CheckBox;
            pnlFramed.Visible = Available.Value == "True" && !frame.Text.ToUpper().Contains( "UNFRAMED" );
            chkUnframed.Checked = !pnlFramed.Visible;
        }
        
        protected void FormView1_PreRender(object sender, EventArgs e)
        {
            
            if(FormView1.DataKey.Values["lastupdated"] is DateTime)
            {
                int id = (int)FormView1.DataKey.Values["id"];
                Page.MetaKeywords = ((HiddenField)FormView1.FindControl( "metatags" )).Value;
                Page.MetaDescription = ((Label)FormView2.FindControl( "lblTitle" )).Text;
                Page.Title = ((Label)FormView2.FindControl( "lblTitle" )).Text;
                RenderPicture( id );
            }
            else // no picture record found
                Response.Redirect( "default.aspx" );

         }

        #region navigation button events
        protected void btnPrevious_Click( object sender, ImageClickEventArgs e )
        {
            int id = (int)FormView1.DataKey.Values["id"];
            int newId = PictureDL.PreviousPublic( id );
            if (newId > 0)
            {
                ProcessPicture( newId );
            }

        }

        protected void btnNext_Click( object sender, ImageClickEventArgs e )
        {
            int id = (int)FormView1.DataKey.Values["id"];
            {
                int newId = PictureDL.NextPublic( id );
                if (newId > 0)
                {
                    ProcessPicture( newId );
                }
            }
        }
        #endregion

        #region PayPal Events
        protected void btnBuy_Click( object sender, ImageClickEventArgs e )
        {
            int id = (int) FormView1.DataKey.Values["id"];
            CheckBox chkUnframed = FormView1.FindControl( "chkUnframed" ) as CheckBox;
            if (GenerateInvoice( id, btnBuy, chkUnframed.Checked ))
            {
                FormView1.Visible = false;
                FormView2.Visible = false;
                pnlProcessing.Visible = true;
                return;
            }
            btnBuy.CancelSubmission();
        }
        #endregion

        
        
    }
}