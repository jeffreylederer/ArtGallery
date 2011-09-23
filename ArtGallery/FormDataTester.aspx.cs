using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace ArtGallery
{
    public partial class FormDataTester : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            StringBuilder test = new StringBuilder();
            foreach (string key in Request.Form)
            {
                test.Append( string.Format( "{0}={1}<br/>", key, Request.Form[key] ) );
            }
            tester.Text = test.ToString();
        }
    }
}