using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace ArtGallery.Account
{
    public partial class ChangeEmail : System.Web.UI.Page
    {

        /// <summary>
        ///  Retrieve current logged on user's emai address and put in label on the page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load( object sender, EventArgs e )
        {
            if (!IsPostBack)
            {
                MembershipUser user = Membership.GetUser();
                lblCurrent.Text = user.Email;
            }
        }

        /// <summary>
        /// This event occurs when user selects submit button. This updates the membership database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click( object sender, EventArgs e )
        {
            MembershipUser user = Membership.GetUser();
            user.Email = txtEmailAddress.Text;
            Membership.UpdateUser( user );
            lblCurrent.Text = txtEmailAddress.Text;
            txtEmailAddress.Text = "";

        }
    }
}