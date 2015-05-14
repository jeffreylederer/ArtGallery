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
        /// <summary>
        /// Determines whether to hide or show the pager control
        /// </summary>
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
 
        /// <summary>
        /// Used to determine if the user has altered the querystring
        /// and change the page title to the name of the gallery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void frmTop_PreRender(object sender, EventArgs e)
        {
            if (frmTop.DataKey.Value is int)
            {
                this.Title = ((Label)frmTop.FindControl("lblTitle")).Text;
            }
            else
                Response.Redirect("default.aspx");
        }

        /// <summary>
        /// Button event used to show or hide the pager. When the pager is
        /// hidden, the entire gallery is listed on the page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// A check to see if there are any pictures in the gallery. If there
        /// are none then disable the button that allows the user to switch from
        /// paged to unpaged.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ListView1_PreRender( object sender, EventArgs e )
        {
            DataPager dataPager = ListView1.FindControl( "dataPager" ) as DataPager;
            if (dataPager == null) // no pictures in gallery
            {
                btnViewAll.Text = "No art work is in this gallery yet.";
                btnViewAll.Enabled = false;
            }
        }
    }
}