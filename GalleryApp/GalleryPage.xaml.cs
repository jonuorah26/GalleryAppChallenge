using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp;


namespace GalleryApp
{
    public partial class GalleryPage : ContentPage
    {
        public ObservableCollection<GalleryImg> GalleryImgs { get; set; } = new ObservableCollection<GalleryImg>();
        public int GalWidth { get; set; }
        public bool isAndroid { get; set; }
        public bool isIOS { get; set; }
       //public ScrollView scroll = new ScrollView();

        public GalleryPage()
        {
            InitializeComponent();
            BindingContext = this;
            if(Device.RuntimePlatform == Device.Android)
            {
                isAndroid = true;
                isIOS = false;
            }
            else
            {
                isAndroid = false;
                isIOS = true;
            }
            OnPropertyChanged(nameof(isAndroid));
            OnPropertyChanged(nameof(isIOS));
        }

        protected override async void OnAppearing()
        {
            await Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    PopulateGallery();    
                });
            });
            
 
        }

        public void PopulateGallery()
        {
            if(GalleryImgs.Count > 0)
            {
                //return instead of repopulating same images
                return;
            }

            //ImageSource imgSrc = ImageSource.FromFile("manofsteel.jpeg");
            ImageSource imgSrc = null;
            //imgSrc = ImageSource.FromResource("GalleryApp.Images.manofsteel.jpeg");
            
            Assembly assembly = GetType().Assembly;
            string[] resources = assembly.GetManifestResourceNames();

            //GalleryImgs.Clear();
            foreach(var resource in resources)
            {
                try
                {
                    if (resource.Contains(".Images."))
                    {
                        imgSrc = ImageSource.FromResource(resource);
                        GalleryImg newImg = new GalleryImg(imgSrc);
                        newImg.width = (int)this.Width / 3;
                        string fileName = resource;
                        for (int i = 0; i < 2; ++i)
                        {
                            int startIndex = fileName.IndexOf(".") + 1;
                            fileName = fileName.Substring(startIndex);
                        }
                        newImg.fileName = fileName;

                        GalleryImgs.Add(newImg);
                    }
                }
                catch(Exception e)
                {
                    string msg = e.Message;
                    Console.WriteLine(msg);
                }
                
            }
            OnPropertyChanged(nameof(GalleryImgs));

            
        }

        async void OnImageTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            GalleryImg selectedImg = null;

            foreach(var img in GalleryImgs)
            {
                if(imageSender.Source == img.src)
                {
                    selectedImg = img;
                }
            }

            await Navigation.PushAsync(new FullPicturePage(selectedImg));
            
        }



        /*
            foreach(string resource in resources)
            {
                
                if(resource.Contains(".assets."))
                {
                    Stream stream = assembly.GetManifestResourceStream(resource);

                    using(SKManagedStream skStream = new SKManagedStream(stream))
                    {
                        SKBitmap bitmap = SKBitmap.Decode(skStream);

                        imgSrc = ImageSource.FromStream(() => new MemoryStream(bitmap.Encode(SKEncodedImageFormat.Png, 100).ToArray()));
                    }
                }
                
            }
            */


        /*
        for (int i=0; i<30; ++i)
        {
            GalleryImg newImg = new GalleryImg(imgSrc);
            newImg.width = (int)this.Width / 3;
            GalleryImgs.Add(newImg);
        }
        //GalWidth = (int)this.Width / 3;

        OnPropertyChanged(nameof(GalleryImgs));

        //scroll.Content = galleryLayout;
        */
    }
}
