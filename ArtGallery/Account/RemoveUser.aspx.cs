using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Security;

namespace ArtGallery.Account
{
    public partial class RemoveUser : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            if (!IsPostBack)
            {
                MembershipUserCollection users = Membership.GetAllUsers();
                MembershipUser currentUser = Membership.GetUser();
                List<SiteUser> list = new List<SiteUser>();

                foreach (MembershipUser user in users)
                {
                    if (currentUser.UserName != user.UserName)
                        list.Add( new SiteUser( user.UserName ) );
                }
                GridView1.DataSource = list;
                GridView1.DataBind();
                up1.Update();
            }
        }

        protected void GridView1_RowCommand( object sender, GridViewCommandEventArgs e )
        {
            if (e.CommandName == "Select")
            {
                int index = int.Parse( e.CommandArgument.ToString() );
                string username = GridView1.DataKeys[index].Value.ToString();
                Membership.DeleteUser( username );
                GridView1.DataBind();
                up1.Update();
            }
        }
    }

    internal class SiteUser
    {
        public string username
        {
            get;
            set;
        }

        public  SiteUser( string _username )
        {
            username = _username;
        }
    }

} 