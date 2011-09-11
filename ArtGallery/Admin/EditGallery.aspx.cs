using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtGallery.DataLayer;

namespace ArtGallery.Admin
{
    public partial class EditGallery : System.Web.UI.Page
    {
        
               
        protected void ObjectDataSource1_Updated( object sender, ObjectDataSourceStatusEventArgs e )
        {
            if (e.Exception == null && (int)e.ReturnValue == 1)
            {
                ErrorLabel.Text = "Saved Successfully";
                up1.Update();
            }

        }

        protected void ObjectDataSource1_Selected( object sender, ObjectDataSourceStatusEventArgs e )
        {
            if(e.ReturnValue == null || (( ArtGalleryDS.GalleryDataTable) e.ReturnValue).Rows.Count == 0)
                Response.Redirect( "~/default.aspx", true );
        }
    }
}