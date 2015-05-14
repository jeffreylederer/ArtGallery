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
    public partial class SelectGallery : System.Web.UI.Page
    {
        /// <summary>
        /// Check to make sure user did not change the query string to point
        /// to non-exist gallery.
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
                Response.Redirect("~/default.aspx");
        }
    }
}