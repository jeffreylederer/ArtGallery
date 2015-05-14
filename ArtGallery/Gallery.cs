using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtGallery
{
    public class Gallery
    {
        public int id { get; set; }
        public string gallerytitle { get; set; }
        public bool active{ get; set; }
        public string menutext { get; set; }
        public DateTime lastupdated { get; set; }
    }
}