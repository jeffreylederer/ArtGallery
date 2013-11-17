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

        private int tries
        {
            get
            {
                if (Session["tries"] == null)
                    Session["tries"] = 1;
                return (int)Session["tries"];
            }
            set
            {
                Session["tries"] = value;
            }
        }

        protected void Page_Load( object sender, EventArgs e )
        {
            CaptchaControl CAPTCHA = LoginUser.FindControl( "CAPTCHA" ) as CaptchaControl;
            CAPTCHA.Visible = tries >= 3;
        }
        protected void LoginUser_Authenticate( object sender, AuthenticateEventArgs e )
        {
            string loginUsername = LoginUser.UserName;
            string loginPassword = LoginUser.Password;
            CaptchaControl CAPTCHA = LoginUser.FindControl("CAPTCHA") as CaptchaControl;

            // first check the Captcha to insure it is valid.
            if (!CAPTCHA.UserValidated && tries >= 3)
            {
                //CAPTCHA invalid
                LoginUser.FailureText = "The code you entered did not match up with the image provided; please try again with this new image.";
                tries++;
                e.Authenticated = false;
            }

            // next check the userid and password
            else if (Membership.ValidateUser( loginUsername, loginPassword ))
            {
                //Only set e.Authenticated to True if ALL checks pass
                e.Authenticated = true;
                tries = 0;
            }

            // else tell user to try again
            else
            {
                tries++;
                e.Authenticated = false;
                LoginUser.FailureText = "Your username and/or password are invalid.";
            }
        }

   
    }
}
