using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Middle_Manager_Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageUpload : ContentView
    {
        public ImageUpload()
        {
            InitializeComponent();
        }

        private MediaFile _mediaFile;

        //I'm not sure what's more useful at the moment, exposing the MediaFile allows for cropping and a range of other features.
        //Byte array is instantly Accessible and usable, will probably keep both but playing around with xamarin controls for the first time.
        //So still getting a feel.

        public Byte[] ImageBytes
        {
            get
            {
                using (var memoryStream = new MemoryStream())
                {
                    _mediaFile.GetStream().CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
        public MediaFile MediaFile => _mediaFile;

        //Picture choose from device    
        private async void btnSelectPic_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                //await DisplayAlert("Error", "This is not support on your device.", "OK");
                return;
            }
            else
            {
                var mediaOption = new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                };
                _mediaFile = await CrossMedia.Current.PickPhotoAsync();
                if (_mediaFile == null) return;
                imageView.Source = ImageSource.FromStream(() => _mediaFile.GetStream());
            }
        }

        //Take picture from camera    
        private async void btnTakePic_Clicked(object sender, EventArgs e)
        {

            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                //await DisplayAlert("No Camera", ":(No Camera available.)", "OK");
                return;
            }
            else
            {
                _mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "Pictures",
                    Name = "my_images.jpg",

                });

                if (_mediaFile == null) return;
                imageView.Source = ImageSource.FromStream(() => _mediaFile.GetStream());
                var mediaOption = new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                };
            }
        }

        public void Busy()
        {
            uploadIndicator.IsVisible = true;
            uploadIndicator.IsRunning = true;
            btnSelectPic.IsEnabled = false;
            btnTakePic.IsEnabled = false;
            //btnUpload.IsEnabled = false;
        }

        public void NotBusy()
        {
            uploadIndicator.IsVisible = false;
            uploadIndicator.IsRunning = false;
            btnSelectPic.IsEnabled = true;
            btnTakePic.IsEnabled = true;
            //btnUpload.IsEnabled = true;
        }
    }
}