using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArtGallery
{
    public class BookController : ApiController
    {
        // GET api/<controller>/id
        public IEnumerable<Book> Get(int id)
        {
            List<Book> list = new List<Book>();
            DataLayer.ArtGalleryDS.BookDataTable bookTable = BookDL.GetPublic(id);
            foreach (DataLayer.ArtGalleryDS.BookRow row in bookTable)
            {
                Book book = new Book();
                book.menutext = row.menutext;
                book.id = row.id;             
                list.Add(book);
            }
            return list;
        }
    }
}