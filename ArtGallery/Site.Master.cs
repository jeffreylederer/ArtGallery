using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


namespace ArtGallery
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Menu menu = (Menu)this.FindControl("NavigationMenu");
            PopulateMenu.Get(menu);
            logo.ImageUrl = "~/Images/" + Application["logo"].ToString();
            if (Request.PathInfo.Contains( "/Admin/" ))
            {
                dvMain.Attributes["class"] = "mainAdmin";
            }
        }

        

        protected void btnSearch_Click( object sender, EventArgs e )
        {
            string str = txtSearch.Text;
            Response.Redirect( "~/SearchResult.aspx?id="+ HttpUtility.UrlEncode(str));
        }
    }
}
