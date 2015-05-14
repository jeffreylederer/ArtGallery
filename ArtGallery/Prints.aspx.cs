using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtGallery
{
    public partial class Prints : Page
    {

        protected void GridView1_RowCommand( object sender, GridViewCommandEventArgs e )
        {
            if (e.CommandName == "Select")
            {
                int index = int.Parse( e.CommandArgument.ToString() );
                int id = (int) GridView1.DataKeys[index].Value;
                Response.Redirect( string.Format( "ShippingPage.aspx?id={0:0}&repro=true",
                id) );
            }
        }

        protected void FormView1_PreRender( object sender, EventArgs e )
        {
            if(!(FormView1.DataKey.Value is int))
                Response.Redirect("~/default.aspx");
        }

    }
}