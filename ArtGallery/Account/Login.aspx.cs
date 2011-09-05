using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using WebControlCaptcha;

namespace ArtGallery.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e )
        {
        }

        protected void LoginUser_Authenticate( object sender, AuthenticateEventArgs e )
        {
            string loginUsername = LoginUser.UserName;
            string loginPassword = LoginUser.Password;
            CaptchaControl CAPTCHA = LoginUser.FindControl("CAPTCHA") as CaptchaControl;
            if (!CAPTCHA.UserValidated)
            {
                //CAPTCHA invalid
                LoginUser.FailureText = "The code you entered did not match up with the image provided; please try again with this new image.";
                e.Authenticated = false;
            }
            else if (Membership.ValidateUser( loginUsername, loginPassword ))
            {
                //Only set e.Authenticated to True if ALL checks pass
                e.Authenticated = true;
            }
            else
            {
                 e.Authenticated = false;
                 LoginUser.FailureText = "Your username and/or password are invalid.";
            }
        }

   
    }
}
