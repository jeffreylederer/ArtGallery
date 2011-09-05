using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web;
using System.Drawing;

namespace ArtGallery
{
    public class ImageWatermarkTransform : ImageTransform
    {
        public string WatermarkText = "";
        public Color FontColor = Color.Black;
        public float FontSize = 14.0f;
        public string FontFamily = "Verdana";

        public override Image ProcessImage(System.Drawing.Image image)
        {
            Font WatermarkFont = new Font(this.FontFamily, this.FontSize);
            Graphics myGraphics = Graphics.FromImage(image);
            SizeF sz = myGraphics.MeasureString(this.WatermarkText, WatermarkFont);
            float X = image.Width / 2 - sz.Width / 2;
            float Y = image.Height / 2 - sz.Height / 2;
            myGraphics.DrawString(this.WatermarkText, WatermarkFont, new SolidBrush(this.FontColor), X, Y);
            return image;
        }
    }
}
