using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtGallery.Account
{
    public partial class RecoverPassword : System.Web.UI.Page
    {
       
        protected void PasswordRecovery1_SendMailError( object sender, SendMailErrorEventArgs e )
        {
            if (e.Exception != null)
            {
                ErrorLabel.Text = "Send Mail Error " + e.Exception.Message;
                e.Handled = true;
            }
            else
                ErrorLabel.Text = "Unknown Send Mail Error";
            up1.Update();
        }

        protected void PasswordRecovery1_UserLookupError( object sender, EventArgs e )
        {
            PasswordRecovery1.Visible = false;
            CAPTCHA.Enabled = true;
            pnlCap.Visible = true;
            PasswordRecovery1.Enabled = false;
            up1.Update();
        }

        protected void btnSubmit_Click( object sender, EventArgs e )
        {
            CAPTCHA.Validate();
            if (CAPTCHA.UserValidated)
            {
                PasswordRecovery1.Visible = true;
                PasswordRecovery1.Enabled = true;
                CAPTCHA.Enabled = false;
                pnlCap.Visible = false;
                up1.Update();
            }
        }
    }
}