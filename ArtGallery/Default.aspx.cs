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
        
        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.Page.MetaKeywords = Application["metatags"].ToString();
            this.Page.MetaDescription = ConfigurationManager.AppSettings["SiteName"].ToString();
            this.Title = ConfigurationManager.AppSettings["SiteName"];
            if(!IsPostBack)
                Timer1_Tick( sender, e );
         }


        protected int GetNext
        {
            get
            {
                return PictureDL.Random();
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            frmTop.DataSource = PictureDL.GetByIdTable( GetNext );
            frmTop.DataBind();
            up1.Update();
        }

        protected void frmTop_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Timer1_Tick(sender, e);
            }
        }
    }

    
}
