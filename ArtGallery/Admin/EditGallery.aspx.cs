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
        protected void FormView1_PreRender( object sender, EventArgs e )
        {
            if (!(FormView1.DataKey["lastupdated"] is DateTime))
                Response.Redirect( "~/Default.aspx" );
        }

        protected void ObjectDataSource1_Updated( object sender, ObjectDataSourceStatusEventArgs e )
        {
            if (e.Exception != null)
                ErrorLogDL.Insert( e.Exception );
        }

        protected void ObjectDataSource1_Deleted( object sender, ObjectDataSourceStatusEventArgs e )
        {
            if (e.Exception != null)
                ErrorLogDL.Insert( e.Exception );
        }
    }
}