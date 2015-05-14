using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace ArtGallery.Account
{
    public partial class RecoverPassword : System.Web.UI.Page
    {
       
        protected void btnSubmit_Click( object sender, EventArgs e )
        {
            try
            {
                CAPTCHA.Validate();
                if (CAPTCHA.UserValidated)
                {
                    MembershipUser user = Membership.GetUser( txtUserName.Text );
                    if (user == null)
                    {
                        ErrorLabel.Text = "Unknown userid";
                    }
                    else
                    {
                        string password = user.ResetPassword();
                        SendMailDL.SendMail( user.Email, user.Email, "Reset password from Art Gallery Site",
                            string.Format( "Your new password is {0}", password ), false );
                        ErrorLabel.Text = "Your new password has been emailed to you.";
                    }
                }
                else
                    ErrorLabel.Text = "The code you entered did not match up with the image provided; please try again with this new image.";
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "Process Error";
                ErrorLogDL.Insert( ex );
            }
            up1.Update();
        }
    }
}