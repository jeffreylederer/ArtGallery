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

                Exception ex1 = Server.GetLastError();
                Server.ClearError();
                if (ex1.Message == "File does not exist.")
                    return;
                if (ex1 != null)
                {
                    int id = ErrorLogDL.Insert( (Exception) ex1 );
                    lblError.Text = id.ToString();
                    return;
                }

            }
            catch { }
            lblError.Text = "Unknown";
        }
    }
}