using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtGallery.DataLayer;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Text;
using SpiceLogic.PayPalCtrlForWPS.Controls;
using SpiceLogic.PayPalCtrlForWPS;
using SpiceLogic.PayPalCtrlForWPS.Core;

namespace ArtGallery
{
    public partial class SearchPicturePage : Page
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
                ProcessPicture( id );
            }
        }

        private void ProcessPicture( int id )
        {
            ArtGalleryDS.PictureDataTable table = PictureDL.GetById( id );
            FormView1.DataSource = table.Rows;
            FormView1.DataBind();

            ArtGalleryDS.Picture_GetWithWaterMarkDataTable table1 = PictureDL.GetWithWaterMark( id );
            FormView2.DataSource = table1.Rows;
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

            Button btnBuy = FormView1.FindControl( "btnBuy" ) as Button;
            // no originals left, but there are prints
            if (rpt.Rows.Count > 0 && Available.Value == "False")
            {
                lblPrice.Visible = false;
                btnBuy.Visible = false;
                lblPrints.Text = rpt[0].description;

            }
            // only originals left
            else if (Available.Value == "True" && rpt.Rows.Count == 0)
            {
                lblSold.Visible = false;
                btnBuy.Visible = true;
                row.Visible = false;

            }
            // prints and originals left
            else if (Available.Value == "True" && rpt.Rows.Count > 0)
            {
                lblSold.Visible = false;
                btnBuy.Visible = true;
                lblPrints.Text = rpt[0].description;
            }

           up1.Update();
        }
        
        protected void FormView1_PreRender(object sender, EventArgs e)
        {
            HiddenField ID = FormView1.FindControl( "ID" ) as HiddenField;
            int id = 0;
            if (int.TryParse( ID.Value, out id ))
            {
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
            List<int> list = (List<int>)Session["search"];
             HiddenField ID = FormView1.FindControl( "ID" ) as HiddenField;
            int id = 0;
            if (int.TryParse( ID.Value, out id ))
            {
                int index = list.FindIndex( x => x == id );
                if (index == 0)
                    index = list.Count - 1;
                else
                    index = index - 1;
                id = list[index];
                ProcessPicture( id );
            }
        }

        protected void btnNext_Click( object sender, ImageClickEventArgs e )
        {
            List<int> list = (List<int>)Session["search"];
             HiddenField ID = FormView1.FindControl( "ID" ) as HiddenField;
            int id = 0;
            if (int.TryParse( ID.Value, out id ))
            {
                int index = list.FindIndex( x => x == id );
                if (index == list.Count - 1)
                    index = 0;
                else
                    index = index + 1;

                id = list[index];
                ProcessPicture( id );
            }
        }
        #endregion

        protected void btnBuy_Click( object sender,EventArgs e )
        {
            int id = (int)FormView1.DataKey.Values["id"];
            Response.Redirect( string.Format( "ShippingPage.aspx?id={0:0}&repro=false",
                id) );
        }
    }
}