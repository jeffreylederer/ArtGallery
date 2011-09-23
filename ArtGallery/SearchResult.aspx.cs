using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtGallery.DataLayer;

namespace ArtGallery
{
    public partial class SearchResult : System.Web.UI.Page
    {
        protected void Page_PreRender( object sender, EventArgs e )
        {

             if (Session["search"] != null)
            {
                List<int> list = Session["search"] as List<int>;
                string strlist = "";
                foreach (int item in list)
                {
                    strlist += item.ToString() + ",";
                }
                ArtGalleryDS.PictureDataTable newtable = SearchWordDL.GetByList( strlist.Substring(0, strlist.Length-1) );
                if (newtable.Rows.Count > 0)
                {
                    lblSearch.Text = "Search Results";
                    ListView1.DataSource = newtable.Rows;
                    ListView1.DataBind();
                    return;
                }
            }
            string id = Request.QueryString["id"];
            if(string.IsNullOrWhiteSpace(id))
            {
                lblSearch.Text = "No Results Found";
                return;
            }
            ArtGalleryDS.PictureDataTable table = SearchWordDL.Get(HttpUtility.UrlDecode( id ));
            if(table.Rows.Count==0)
            {
                lblSearch.Text = "No Results Found";
            }
            else
            {
                lblSearch.Text = "Search Results";
                ListView1.DataSource = table.Rows;
                ListView1.DataBind();
                List<int> list = new List<int>();
                foreach (ArtGalleryDS.PictureRow row in table)
                {
                    list.Add( row.id );
                }

                Session["search"] = list;
            }

        }
    }
}