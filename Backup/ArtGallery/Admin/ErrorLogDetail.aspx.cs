using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtGallery.Admin
{
    public partial class ErrorLogDetail : System.Web.UI.Page
    {

        /// <summary>
        /// Convert cr-lf to html break
        /// </summary>
        /// <param name="val">value in errormessage column</param>
        /// <returns>updated string</returns>
        protected string Split( object val )
        {
            return ((string)val).Replace( "\r\n", "<br/>" );
        }
    }
}