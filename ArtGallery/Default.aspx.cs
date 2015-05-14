using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;

namespace ArtGallery
{
    public partial class DefaultPage : System.Web.UI.Page
    {
        protected string HomePageText;
        /// <summary>
        /// Just before the page is rendered, put in the metatags for the site on the page,
        /// make the site name the Meta Description and the page title.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Page.MetaKeywords = Application["metatags"].ToString();
                this.Page.MetaDescription = ConfigurationManager.AppSettings["SiteName"].ToString();
                this.Title = ConfigurationManager.AppSettings["SiteName"];
                Timer1_Tick( sender, e );
            }
         }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataLayer.ArtGalleryDS.SiteDataTable table = SiteDL.Get();
                DataLayer.ArtGalleryDS.SiteRow row = table.Rows[0] as DataLayer.ArtGalleryDS.SiteRow;
                HomePageText = row.HomePageText;
            }
        }


        
        /// <summary>
        /// This event occurs when the timer fires. A random picture is displayed
        /// in the right side of page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            frmTop.DataSource = PictureDL.GetWithWaterMark( PictureDL.Random() );
            frmTop.DataBind();
            up1.Update();
        }

        /// <summary>
        /// Start the timer that rotates the picture on the home page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void frmTop_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Timer1_Tick(sender, e);
            }
        }
    }

    
}
