using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtGallery.DataLayer;
using System.IO;

namespace ArtGallery.Admin
{
    public partial class OrphanPictures : System.Web.UI.Page
    {

        /// <summary>
        /// This called when user selects the Remove row command in the grid. It removes a picture file from
        /// the App_Data directory. Only files not associated with a picture record are displayed in the grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowCommand( object sender, GridViewCommandEventArgs e )
        {
            if (e.CommandName == "Remove")
            {
                int index = int.Parse(e.CommandArgument.ToString());

                string filename = Request.MapPath( "~/App_Data/" + (string)GridView1.DataKeys[index].Value );
                FileInfo fi = new FileInfo( filename );
                fi.Delete();
                GridView1.DataBind();
            }
        }
    }
}