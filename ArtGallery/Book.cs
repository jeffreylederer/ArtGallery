using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtGallery
{
    public class Book
    {
        public int id { get; set; }
        public string booktitle { get; set; }
        public bool active { get; set; }
        public string menutext { get; set; }
        public DateTime lastupdated { get; set; }
    }
}