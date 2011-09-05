using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using Microsoft.Web;
using System.Web;
using System.Collections.Specialized;

namespace ArtGallery
{
    /// <summary>
    /// Summary description for ImageHandler
    /// </summary>
    public class PageImageHandler : ImageHandler
    {

        public PageImageHandler()
        {
            base.ContentType = ImageFormat.Jpeg;
            base.EnableServerCache = false;
        }


        public override ImageInfo GenerateImage(NameValueCollection parameters)
        {
            //Get the parameters
            string imageFile = HttpContext.Current.Server.MapPath( "~/App_Data/Missing.jpg" );
            if (!String.IsNullOrEmpty( parameters["ImageUrl"] ))
            {
                string imageUrl = parameters["ImageUrl"];
                imageFile = HttpContext.Current.Server.MapPath( imageUrl );
            }
            FileInfo fi = new FileInfo(imageFile);
            if (!fi.Exists)
            {
                imageFile = HttpContext.Current.Server.MapPath("~/App_Data/Missing.jpg");
            }

            if (HttpContext.Current.Session != null && HttpContext.Current.Session["picture"] != null)
            {
                imageFile = HttpContext.Current.Session["picture"] as string;
                HttpContext.Current.Session["picture"] = null;

            }
            FileStream fs = new FileStream(imageFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            int fileWidth, fileHeight;
            using (System.Drawing.Image image = System.Drawing.Image.FromStream( fs ))
            {
                fileWidth = image.Width;
                fileHeight = image.Height;
            }
            fs.Close();

            // Add the resize transform logic (if needed)
            ImageResizeTransform resizeTrans = new ImageResizeTransform();
            if (fileHeight <= fileWidth)
            {
                resizeTrans.Width = Convert.ToInt32(parameters["Width"]);
                base.ImageTransforms.Add(resizeTrans);
            }
            else
            {
                resizeTrans.Height = Convert.ToInt32(parameters["Height"]);
                base.ImageTransforms.Add(resizeTrans);
            }

            // Add the watermark transform logic (if needed) 
            if (!string.IsNullOrEmpty(parameters["WatermarkText"]))
            {

                ImageWatermarkTransform watermarkTrans = new ImageWatermarkTransform();
                watermarkTrans.WatermarkText = parameters["WatermarkText"];

                if (!String.IsNullOrEmpty(parameters["WatermarkFontFamily"]))
                    watermarkTrans.FontFamily = parameters["WatermarkFontFamily"];

                if (!String.IsNullOrEmpty(parameters["WatermarkFontColor"]))
                    watermarkTrans.FontColor = ColorTranslator.FromHtml(parameters["WatermarkFontColor"]);

                if (!String.IsNullOrEmpty(parameters["WatermarkFontSize"]))
                    watermarkTrans.FontSize = Convert.ToSingle(parameters["WatermarkFontSize"]);

                base.ImageTransforms.Add(watermarkTrans);
                if (!File.Exists(imageFile))
                    throw new ArgumentException(String.Format("The file {0} could not be found", imageFile));
            }
            return new ImageInfo(File.ReadAllBytes(imageFile));

        }

    }
}
