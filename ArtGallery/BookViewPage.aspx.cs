using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ArtGallery.DataLayer;


namespace ArtGallery
{
    public partial class BookViewPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region navigation button events
        protected void btnPrevious_Click(object sender, ImageClickEventArgs e)
        {
            int id;
            if (int.TryParse(Request.QueryString["id"], out id))
            {
                int newId = BookPageDL.PreviousPublic(id);
                if (newId > 0)
                {
                    ProcessPicture(newId);
                }
            }

        }

        protected void btnNext_Click(object sender, ImageClickEventArgs e)
        {
             int id;
            if (int.TryParse(Request.QueryString["id"], out id))
            {
                int newId = BookPageDL.NextPublic(id);
                if (newId > 0)
                {
                    ProcessPicture(newId);
                }
            }
        }
        #endregion

        private void ProcessPicture(int id)
        {

            ArtGalleryDS.BookPage_GetWithWaterMarkDataTable table1 = BookPageDL.GetWithWaterMark(id);
            FormView2.DataSource = table1.Rows;
            FormView2.DataBind();
        }
    }
}