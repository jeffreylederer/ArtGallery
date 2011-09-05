using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace ArtGallery.Admin
{
    public partial class AddPicture : System.Web.UI.Page
    {
        
        

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(FileUpload1.FileName);
                    FileUpload1.SaveAs(Server.MapPath("~/App_Data/") + filename);
                    ErrorLabel.Text = "Upload status: File uploaded!";
                    ((TextBox)FormView1.FindControl("lblPicturePath")).Text = filename;
                }
                catch (Exception ex)
                {
                    ErrorLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
                up1.Update();
                
            }
        }

        protected void FormView1_PreRender( object sender, EventArgs e )
        {
            if (!IsPostBack)
            {
                ((CheckBox)FormView1.FindControl( "chkAvailble" )).Checked = true;
                string idStr = Request.QueryString["id"];
                if (string.IsNullOrWhiteSpace( idStr ))
                    return;
                int id;
                if (!int.TryParse( idStr, out id ))
                    return;
                DropDownList DropDownList1 = FormView1.FindControl("DropDownList1") as DropDownList;
                if (id < 0 || id > DropDownList1.Items.Count - 1)
                    return;
                DropDownList1.SelectedIndex = id;
            }
        }

        protected void ObjectDataSource1_Inserted( object sender, ObjectDataSourceStatusEventArgs e )
        {
            if (e.Exception != null)
            {
                Exception ex = e.Exception;
                while (ex != null)
                {
                    ErrorLabel.Text = e.Exception.Message;
                    ex = ex.InnerException;
                }
                e.ExceptionHandled = true;
                up1.Update();
                return;
            }
            int id = (int)e.ReturnValue;
            Response.Redirect( "EditPicture.aspx?id=" + id.ToString() );
        }

               
        

   }
}