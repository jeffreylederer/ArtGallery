using System;
using System.Web.Security;
using ArtGallery.DataLayer;
using System.Web.Http;
using System.Web.Routing;

namespace ArtGallery
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            try
            {
                ArtGalleryDS.SiteDataTable table = SiteDL.Get();
                if (table.Rows.Count != 1)
                    throw new Exception( "Could not get site information" );
                Application["logo"] = table[0].LogoPath;
                Application["metatags"] = table[0].Metatags;
                Application["email"] = table[0].email;

                //// Add Administrator.
                //if (!Roles.RoleExists("Admin"))
                //{
                //    Roles.CreateRole("Admin");
                //}
                //if (Membership.GetUser("ArtAdmin") == null)
                //{
                //    Membership.CreateUser("ArtAdmin", "Ilene#13$", "jeffrey@winnlederer.com");
                //    Roles.AddUserToRole("ArtAdmin", "Admin");
                //}

                RouteTable.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = System.Web.Http.RouteParameter.Optional }
            );


            }
            catch
            {
                Response.Redirect( "~/ErrorPage.aspx" );
            }
            
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
