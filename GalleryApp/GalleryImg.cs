using System;
using Xamarin.Forms;

namespace GalleryApp
{
    public class GalleryImg
    {
        public ImageSource src { get; set; }
        public int width { get; set; }
        public string fileName { get; set; }

        public GalleryImg(ImageSource src, string name = "", int width = 0)
        {
            this.src = src;
            this.width = width;
            this.fileName = name;
        }
    }
}
