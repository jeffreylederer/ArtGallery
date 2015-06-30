using System.Web.Http;
using System.Data;
using System.Collections.Generic;

namespace ArtGallery
{
    public class GalleryController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Gallery> Get()
        {
            List<Gallery> list = new List<Gallery>();
            DataLayer.ArtGalleryDS.GalleryDataTable galleryTable = GalleryDL.GetPublic();
            foreach (DataLayer.ArtGalleryDS.GalleryRow row in galleryTable)
            {
                Gallery gallery = new Gallery();
                gallery.active = row.active;
                gallery.menutext = row.menutext;
                gallery.id = row.id;
                gallery.lastupdated = row.lastupdated;
                gallery.gallerytitle = row.gallerytitle;
                list.Add(gallery);
            }
            return list;
        }

    }
}