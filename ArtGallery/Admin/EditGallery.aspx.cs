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

        /// <summary>
        /// protects program against a user changing the query string to a nonexistance gallery
        /// record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selected( object sender, ObjectDataSourceStatusEventArgs e )
        {
            if(e.ReturnValue == null || (( ArtGalleryDS.GalleryDataTable) e.ReturnValue).Rows.Count == 0)
                Response.Redirect( "~/default.aspx", true );
        }
    }
}