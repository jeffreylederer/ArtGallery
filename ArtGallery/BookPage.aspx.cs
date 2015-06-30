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
                ArtGalleryDS.BookDataTable table = BookDL.GetById(id);
                FormView1.DataSource = table.Rows;
                FormView1.DataBind();

                var table1 = BookDL.GetWithWaterMark(id);
                FormView2.DataSource = table1.Rows;
                FormView2.DataBind();


                frmSingleImage.DataBind();
                ListView1.DataBind();
                upList.Update();
            }
            if (Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"].Contains("refImage"))
            {
                hdSingle.Value = Request.Form["__EVENTARGUMENT"];
                frmSingleImage.DataBind();
                ListView1.DataBind();
                upList.Update();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                var id = int.Parse(Request.QueryString["id"]);
                var table = BookPageDL.GetByBookIdPublic(id);
                btnInside.Visible = table != null && table.Rows.Count > 0;
                if (btnInside.Visible)
                {
                    ArtGalleryDS.BookPageRow row = (ArtGalleryDS.BookPageRow)table.Rows[0];
                    hdSingle.Value = row.id.ToString();
                    frmSingleImage.DataBind();
                }
                else
                    pnlPopup.Visible = false;

            }
        }


        private void ProcessBook(int id)
        {
            Response.Redirect("BookPage.aspx?id=" + id.ToString());
        }
       
        protected void FormView1_PreRender(object sender, EventArgs e)
        {
            
            if(!(FormView1.DataKey.Values["lastupdated"] is DateTime))
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

        #region navigation button events
        protected void btnPrevious_Click(object sender, ImageClickEventArgs e)
        {
            int id = (int)FormView1.DataKey.Values["id"];
            int newId = BookDL.PreviousPublic(id);
            if (newId > 0)
            {
                ProcessBook(newId);
            }

        }

        protected void btnNext_Click(object sender, ImageClickEventArgs e)
        {
            int id = (int)FormView1.DataKey.Values["id"];
            {
                int newId = BookDL.NextPublic(id);
                if (newId > 0)
                {
                    ProcessBook(newId);
                }
            }
        }

        
        #endregion

       
        #region Popup
          protected void odsSingleImage_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["id"] = int.Parse(hdSingle.Value);
            upPage.Update();
        }

        
        #endregion

    }
}