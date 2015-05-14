using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtGallery
{
    public partial class Email : System.Web.UI.UserControl
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            if (!IsPostBack)
            {
                lblEmail.Text = (string)HttpContext.Current.Application["email"];
                aEmail.Attributes.Add("href", "mailto:" + lblEmail.Text + "?subject=Art Gallery Site");
            }
        }
    }
}