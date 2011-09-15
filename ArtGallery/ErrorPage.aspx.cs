using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtGallery
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Exception ex = Server.GetLastError();
                int id = ErrorLogDL.Insert( ex );
                lblError.Text = id.ToString();
            }
            catch
            {
                lblError.Text = "Unknown";
            }
        }
    }
}