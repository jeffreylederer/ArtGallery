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
    public partial class BookPage : Page
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
                ProcessBook( id );
            }
        }

        private void ProcessBook(int id)
        {
            ArtGalleryDS.BookDataTable table = BookDL.GetById( id );
            FormView1.DataSource = table.Rows;
            FormView1.DataBind();

            ArtGalleryDS.Picture_GetWithWaterMarkDataTable table1 = BookDL.GetWithWaterMark(id);
            FormView2.DataSource = table1.Rows;
            FormView2.DataBind();

        }


        
        
        protected void FormView1_PreRender(object sender, EventArgs e)
        {
            
            if(FormView1.DataKey.Values["lastupdated"] is DateTime)
            {
                int id = (int)FormView1.DataKey.Values["id"];
                              
            }
            else // no picture record found
                Response.Redirect( "default.aspx" );

         }

        protected void FormView2_PreRender( object sender, EventArgs e )
        {
            Page.MetaDescription = ((Label)FormView2.FindControl( "lblTitle" )).Text;
            Page.Title = ((Label)FormView2.FindControl( "lblTitle" )).Text;
        }

  

        #region PayPal Events
        protected void btnBuy_Click( object sender, EventArgs e )
        {
            int id = (int) FormView1.DataKey.Values["id"];
           

            Response.Redirect( string.Format( "ShippingBookPage.aspx?id={0:0}", id) );
        }
        #endregion
        
    }
}