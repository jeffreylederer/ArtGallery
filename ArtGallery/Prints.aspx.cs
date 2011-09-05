using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtGallery
{
    public partial class Prints : InvoicePage
    {
        protected void GridView1_RowCommand( object sender, GridViewCommandEventArgs e )
        {
            if (e.CommandName == "Select")
            {
                btnBuy.BusinessEmailOrMerchantID = (string)Application["ppAccount"];
                int index = int.Parse( e.CommandArgument.ToString() );
                int id = (int) GridView1.DataKeys[index].Value;
                if (GenerateInvoiceReproduction( id, btnBuy ))
                {
                    FormView1.Visible = false;
                    FormView2.Visible = false;
                    GridView1.Visible = false;
                }
            }
        }
    }
}