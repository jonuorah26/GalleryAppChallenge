using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GalleryApp
{
    public partial class FullPicturePage : ContentPage
    {
        public ImageSource Src { get; set; }
        public string ImgTitle { get; set; }

        public FullPicturePage(GalleryImg img)
        {
            InitializeComponent();

            BindingContext = this;

            Src = img.src;
            ImgTitle = img.fileName;
            OnPropertyChanged(nameof(Src));
            OnPropertyChanged(nameof(ImgTitle));
            

        }
    }
}
