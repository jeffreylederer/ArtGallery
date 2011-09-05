using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using Microsoft.Web;
using System.Web;


namespace ArtGallery
{
    /// <summary>
    /// Summary description for ImageHandler
    /// </summary>
    public class SmallImageHandler : ImageHandler
    {

        public SmallImageHandler()
        {
            base.ContentType = ImageFormat.Jpeg;
            base.EnableServerCache = true;

        }


        public override ImageInfo GenerateImage(System.Collections.Specialized.NameValueCollection parameters)
        {
            //Get the parameters

            if (String.IsNullOrEmpty(parameters["ImageUrl"]))
                throw new ArgumentException("You must supply the ImageUrl parameter");

            string imageUrl = parameters["ImageUrl"];
            string imageFile = HttpContext.Current.Server.MapPath(imageUrl);
            FileInfo fi = new FileInfo(imageFile);
            if (!fi.Exists)
            {
                imageFile = HttpContext.Current.Server.MapPath("~/App_Data/Missing.jpg");
            }

            // Add the resize transform logic (if needed)
            ImageResizeTransform resizeTrans = new ImageResizeTransform();
            if (!String.IsNullOrEmpty(parameters["Width"]))
            {
                resizeTrans.Width = Convert.ToInt32(parameters["Width"]);
                base.ImageTransforms.Add(resizeTrans);
            }
            else
            {
                resizeTrans.Height = Convert.ToInt32(parameters["Height"]);
                base.ImageTransforms.Add(resizeTrans);
            }
            this.ImageTransforms.Add(resizeTrans);

            return new ImageInfo(File.ReadAllBytes(imageFile));
        }
    }
}
