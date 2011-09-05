using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ArtGallery.DataLayer;


namespace ArtGallery
{
    public partial class GalleryPage : System.Web.UI.Page
    {

        private bool isPaged
        {
            set
            {
                ViewState["Paged"] = value;
            }
            get
            {
                if (ViewState["Paged"] == null)
                {
                    ViewState["Paged"] = true;
                }
                return (bool)ViewState["Paged"];
            }
        }
 
        protected void frmTop_PreRender(object sender, EventArgs e)
        {
            if (frmTop.DataKey.Value is int)
            {
                this.Title = ((Label)frmTop.FindControl("lblTitle")).Text;
            }
            else
                Response.Redirect("default.aspx");
        }

        protected void btnViewAll_Click( object sender, EventArgs e )
        {
            DataPager dataPager = ListView1.FindControl( "dataPager" ) as DataPager;
            if (isPaged)
            {
                dataPager.PageSize = 1000;
                btnViewAll.Text = "View Paged";
                dataPager.Visible = false;
            }
            else
            {
                dataPager.PageSize = 30;
                btnViewAll.Text = "View All";
                dataPager.Visible = true;

            }
            isPaged = !isPaged;
            ListView1.DataBind();
        }
    }
}