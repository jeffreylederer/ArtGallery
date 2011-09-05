using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtGallery.Admin
{
    public partial class AddGallery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
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
                return;
            }
            int id = (int)e.ReturnValue;
            Application["keywords"] = null;
            Response.Redirect("EditGallery.aspx?id=" + id.ToString());
        }
    }
}