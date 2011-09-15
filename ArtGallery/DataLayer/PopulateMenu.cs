using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using ArtGallery.DataLayer;
using System.Web.Security;


namespace ArtGallery
{
    public static class PopulateMenu
    {
        public static void Get(Menu menu)
        {
            ArtGalleryDS.GalleryDataTable galleryTable = GalleryDL.GetPublic();
            DataSet ds = new DataSet();
            
            MenuItem HomeItem = new MenuItem("Home",null,null,"~/Default.aspx");
            menu.Items.Add(HomeItem);
 
            MenuItem GalleryItem = new MenuItem("Galleries");
            foreach (ArtGalleryDS.GalleryRow row in galleryTable)
            {
                MenuItem Item = new MenuItem(row.menutext,null,null,"~/GalleryPage.aspx?id=" + row.id.ToString());
                GalleryItem.ChildItems.Add(Item);
            }
            menu.Items.Add(GalleryItem);

             
 
            if(Roles.IsUserInRole("Admin"))
            {
                MenuItem AdminItem = new MenuItem("Admin");
                ArtGalleryDS.GalleryDataTable galleryTableAdmin = GalleryDL.Get();
                AdminItem.ChildItems.Add( (new MenuItem( "Setup Site", null, null, "~/Admin/Setup.aspx" )) );

                MenuItem AdminGalleryItem = new MenuItem("Select Gallery to Edit Pictures");
                foreach (ArtGalleryDS.GalleryRow row in galleryTableAdmin)
                {
                    MenuItem Item = new MenuItem(row.menutext, null, null, "~/Admin/SelectGallery.aspx?id=" + row.id.ToString());
                    AdminGalleryItem.ChildItems.Add(Item);
                }
               
                AdminItem.ChildItems.Add(AdminGalleryItem);

                MenuItem AdminEditGalleryItem = new MenuItem("Edit Gallery Info");
                foreach (ArtGalleryDS.GalleryRow row in galleryTableAdmin)
                {
                    MenuItem Item = new MenuItem(row.menutext, null, null, "~/Admin/EditGallery.aspx?id=" + row.id.ToString());
                    AdminEditGalleryItem.ChildItems.Add(Item);
                }
                MenuItem Item1 = new MenuItem( "Add new Gallery", null, null, "~/Admin/AddGallery.aspx" );
                AdminEditGalleryItem.ChildItems.Add( Item1 );
                AdminItem.ChildItems.Add(AdminEditGalleryItem);

                AdminItem.ChildItems.Add((new MenuItem("Add Picture", null, null, "~/Admin/AddPicture.aspx")));
                AdminItem.ChildItems.Add( (new MenuItem( "Remove Orphan Pictures", null, null, "~/Admin/OrphanPictures.aspx" )) );
                AdminItem.ChildItems.Add( (new MenuItem( "Report", null, null, "~/Admin/ReportPage.aspx" )) );

                MenuItem AdminAccount = new MenuItem( "Administator Accounts" );
                AdminAccount.ChildItems.Add( (new MenuItem( "Change Password", null, null, "~/Account/ChangePassword.aspx" )) );
                AdminAccount.ChildItems.Add( (new MenuItem( "Change Email Address", null, null, "~/Account/ChangeEmail.aspx" )) );
                AdminAccount.ChildItems.Add( (new MenuItem( "Add New Administator", null, null, "~/Account/Register.aspx" )) );
                 AdminAccount.ChildItems.Add( (new MenuItem( "Remove Administator", null, null, "~/Account/RemoveUser.aspx" )) );
                AdminItem.ChildItems.Add( AdminAccount );

                AdminItem.ChildItems.Add( (new MenuItem( "View Error Log", null, null, "~/Admin/ErrorLog.aspx" )) );
                AdminItem.ChildItems.Add( (new MenuItem( "Email Server", null, null, "~/Admin/Sendmail.aspx" )) );
                
                menu.Items.Add(AdminItem);
            }
            
            
            MenuItem AboutItem = new MenuItem("About the Artist", null, null,"~/about.aspx");
            menu.Items.Add(AboutItem);
            MenuItem EMailItem = new MenuItem( "Contact", null, null, "~/Contact.aspx" );
            menu.Items.Add( EMailItem );
        }
    }
 }