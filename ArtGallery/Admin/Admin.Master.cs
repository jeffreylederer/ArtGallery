using System;
using System.Web;
using System.Web.Security;

namespace ArtGallery.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            logo.ImageUrl = "~/Images/" + Application["logo"].ToString();
            if (Request.PathInfo.Contains("/Admin/"))
            {
                dvMain.Attributes["class"] = "mainAdmin";
            }
           
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string str = txtSearch.Text;
            Session["search"] = null;
            Response.Redirect("~/SearchResult.aspx?id=" + HttpUtility.UrlEncode(str));
        }
    }
}