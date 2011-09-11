using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtGallery.Admin
{
    public partial class EditGallery : System.Web.UI.Page
    {
        
        protected void ObjectDataSource1_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
                Response.Redirect("~/default.aspx", true);
        }

        protected void FormView1_PreRender(object sender, EventArgs e)
        {
            if (FormView1.DataKey.Values.Count == 0)
            {
                Response.Redirect( "~/default.aspx" );
                up1.Update();
            }
        }

        protected void ObjectDataSource1_Updated( object sender, ObjectDataSourceStatusEventArgs e )
        {
            if (e.Exception == null && (int)e.ReturnValue == 1)
            {
                ErrorLabel.Text = "Saved Successfully";
                up1.Update();
            }

        }
    }
}