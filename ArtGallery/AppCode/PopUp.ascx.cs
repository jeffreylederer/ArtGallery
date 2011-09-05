using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtGallery
{
    public partial class PopUp : System.Web.UI.UserControl
    {
         
        public string Key
        {
            get
            {
                if (ViewState["key"] == null)
                    return "";
                return (string)ViewState["key"];
            }

            set
            {
                ViewState["key"] = value;
            }
        }

        protected string Result
        {
            get
            {
                string str = (string)GetGlobalResourceObject( "HelpText", Key );
                //Panel1.Height = str.Length / 8+ 10;
                return str;
            }
        }
            

        protected void Page_PreRender( object sender, EventArgs e )
        {

            // Set the BehaviorID
            string behaviorID = PopupControlExtender1.ClientID;
            PopupControlExtender1.BehaviorID = behaviorID;

            // Add the clie nt-side attributes (onmouseover & onmouseout)
            string OnMouseOverScript = string.Format( "$find('{0}').showPopup();", behaviorID );
            string ClickScript = string.Format( "$find('{0}').hidePopup();", behaviorID );

            Image1.Attributes.Add( "onclick", OnMouseOverScript );
            impClose.Attributes.Add( "onclick", ClickScript );
        }
    }
}