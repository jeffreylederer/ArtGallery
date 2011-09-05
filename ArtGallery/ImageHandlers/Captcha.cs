using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CustomControls
{

    public class Captcha : Control
    {
        Image _imgCaptcha;

        public string Text
        {
            get
            {
                if (HttpContext.Current.Session["Captcha"] != null)
                {
                    return HttpContext.Current.Session["Captcha"].ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );

            string[] strArray = new string[36];
            strArray = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            Random autoRand = new Random();
            string strCaptcha = string.Empty;
            for (int i = 0; i < 6; i++)
            {
                int j = Convert.ToInt32( autoRand.Next( 0, 62 ) );
                strCaptcha += strArray[j].ToString();
            }

            HttpContext.Current.Session.Add( "Captcha", strCaptcha );

            _imgCaptcha = new Image();
            _imgCaptcha.ImageUrl = "~/CaptchaHandler.ashx";//Image URL is set to the generic handler created in Step1
            this.Controls.Add( _imgCaptcha );
        }
    }
}