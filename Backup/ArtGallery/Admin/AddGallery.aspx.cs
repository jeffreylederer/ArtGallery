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
        /// <summary>
        /// If no error, sends user to edit gallery page in order the created db record.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                ErrorLabel.Text = e.Exception.Message;
                e.ExceptionHandled = true;
                return;
            }
            string id = e.ReturnValue.ToString();
            Application["keywords"] = null;
            Response.Redirect("EditGallery.aspx?id=" + id, true);
        }
    }
}