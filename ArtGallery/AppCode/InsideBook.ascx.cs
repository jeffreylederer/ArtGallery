using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtGallery.DataLayer;

namespace ArtGallery.AppCode
{
    public partial class InsideBook : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["SelectedImage"] = 0;
        }

        public void ShowPopUp(string title)
        {
            lblTitle.Text = title;
            upTitle.Update();
            MPE.Show();
        }

        protected void Click_CancelButton(object sender, EventArgs e)
        {
            MPE.Hide();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            var button = (ImageButton)sender;
            int index = int.Parse(button.CommandArgument);
            ViewState["SelectedImage"] = index;
            frmSingleImage.DataBind();
        }

        protected void odsSingleImage_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["id"] = (int)ViewState["SelectedImage"];
        }

        void odsList_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            ArtGalleryDS.BookPageDataTable table = (ArtGalleryDS.BookPageDataTable)e.ReturnValue;
            if (table != null && table.Rows.Count > 0)
            {
                ArtGalleryDS.BookPageRow row = (ArtGalleryDS.BookPageRow) table.Rows[0];
                ViewState["SelectedImage"] = row.id;
                frmSingleImage.DataBind();
            }
        }

        protected void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var button = (ImageButton)sender;
            if (button.CommandName == "Select")
            {
                int index = int.Parse(button.CommandArgument);
                ViewState["SelectedImage"] = index;
                frmSingleImage.DataBind();
            }
        }

        
    }
}