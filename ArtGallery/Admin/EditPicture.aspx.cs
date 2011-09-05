﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtGallery.DataLayer;
using System.IO;


namespace ArtGallery
{
    public partial class EditPicture : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             FormView2.DataBind();
        }

 
        protected void ObjectDataSource1_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
                Response.Redirect( "~/default.aspx" );
        }

        protected void FormView1_PreRender(object sender, EventArgs e)
        {
            if (!(FormView1.DataKey.Value is int))
                Response.Redirect("~/default.aspx");
        }

        protected void UploadButton_Click( object sender, EventArgs e )
        {
            if (FileUpload1.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName( FileUpload1.FileName );
                    FileUpload1.SaveAs( Server.MapPath( "~/App_Data/" ) + filename );
                    ErrorLabel.Text = "Upload status: File uploaded!";
                    PictureDL.UpdatePicturePath((int) FormView1.DataKey.Value, filename );
                    FormView1.DataBind();
                }
                catch (Exception ex)
                {
                    ErrorLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
                up2.Update();
            }
        }

        protected void btnPrevious_Command( object sender, CommandEventArgs e )
        {
            int id = (int)FormView1.DataKey.Value;
            int galleryid = int.Parse( Request.QueryString["id"] );
            int newId = PictureDL.Previous( id );
            if (newId > 0)
            {
                Response.Redirect( "EditPicture.aspx?id=" + newId.ToString() );
            }
        }

        protected void btnNext_Click( object sender, EventArgs e )
        {
            int id = (int)FormView1.DataKey.Value;
            int galleryid = int.Parse( Request.QueryString["id"] );
            int newId = PictureDL.Next( id );
            if (newId > 0)
            {
                Response.Redirect( "EditPicture.aspx?id=" + newId.ToString() );
            }
        }

        protected void odsReproduction_Inserting( object sender, ObjectDataSourceMethodEventArgs e )
        {
            TextBox handling = (TextBox)GridView1.FooterRow.FindControl( "txtHandling" ) as TextBox;
            TextBox Packing = (TextBox)GridView1.FooterRow.FindControl( "txtPacking" ) as TextBox;
            e.InputParameters["pictureid"] = (int) FormView1.DataKey.Value;
            e.InputParameters["description"] = ((TextBox)GridView1.FooterRow.FindControl( "txtDescription" )).Text;
            e.InputParameters["price"] = decimal.Parse( ((TextBox)GridView1.FooterRow.FindControl( "txtPrice" )).Text );
            e.InputParameters["weight"] = decimal.Parse( ((TextBox)GridView1.FooterRow.FindControl( "txtWeight" )).Text );
            e.InputParameters["handling"] = handling.Text == "" ? (decimal?) null : decimal.Parse(handling.Text);
            e.InputParameters["width"] = decimal.Parse( ((TextBox)GridView1.FooterRow.FindControl( "txtWidth" )).Text );
            e.InputParameters["height"] = decimal.Parse( ((TextBox)GridView1.FooterRow.FindControl( "txtHeight" )).Text );
            e.InputParameters["packingweight"] = Packing.Text == "" ? (double?)null : double.Parse( Packing.Text );
        }
   }
}