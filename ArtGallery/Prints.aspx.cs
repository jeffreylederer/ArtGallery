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

        //protected void Page_Load( object sender, EventArgs e )
        //{
        //    if (!IsPostBack)
        //        SetupButton( btnBuy );
        //}


        protected void GridView1_RowCommand( object sender, GridViewCommandEventArgs e )
        {
            if (e.CommandName == "Select")
            {
                int index = int.Parse( e.CommandArgument.ToString() );
                int id = (int) GridView1.DataKeys[index].Value;
                if (GenerateInvoiceReproduction( id, btnBuy ))
                {
                    FormView1.Visible = false;
                    FormView2.Visible = false;
                    GridView1.Visible = false;
                    divBack.Visible = false;
                    pnlProcessing.Visible = true;
                    btnBuy.Submit();
                    return;
                }
            }
        }

        protected void FormView1_PreRender( object sender, EventArgs e )
        {
            if(!(FormView1.DataKey.Value is int))
                Response.Redirect("~/default.aspx");
        }

    }
}