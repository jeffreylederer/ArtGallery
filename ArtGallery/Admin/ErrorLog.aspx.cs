using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtGallery.Admin
{
    public partial class ErrorLog : System.Web.UI.Page
    {
        /// <summary>
        /// Handles event when user selects the view detail button. This send user to the ErrorLogDetail page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click( object sender, EventArgs e )
        {
            Response.Redirect( "ErrorLogDetail.aspx?id=" + txtid.Text );
        }

        /// <summary>
        /// Convert cr-lf to html break
        /// </summary>
        /// <param name="val">value in errormessage column</param>
        /// <returns>updated string</returns>
        protected string Split( object val )
        {
            return ((string) val).Replace("\r\n", "<br/>");
        }
    }
}