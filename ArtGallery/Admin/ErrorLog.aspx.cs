﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtGallery.Admin
{
    public partial class ErrorLog : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e )
        {

        }

        protected void btnSubmit_Click( object sender, EventArgs e )
        {
            Response.Redirect( "ErrorLogDetail.aspx?id=" + txtid.Text );
        }
    }
}