using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtGallery.Admin
{
    public partial class SendMail : System.Web.UI.Page
    {

        protected void ObjectDataSource1_Updated( object sender, ObjectDataSourceStatusEventArgs e )
        {
            if (e.Exception == null)
                ErrorLabel.Text = "Saved Successfully";
        }

        protected void Button1_Click( object sender, EventArgs e )
        {
            if (SendMailDL.SendMail( "Test Email from Art Gallery Site", "This is a test of the Art Gallery Site Email Method", false ))
                ErrorLabel.Text = "Sent";
            else
                ErrorLabel.Text = "Failed to Send";
        }
    }
}