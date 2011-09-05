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