using Android.Content;
using ObywatelPlus.Droid;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ObywatelPlus
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Damian : ContentPage
	{
		public Damian ()
		{
			InitializeComponent ();
            CameraButton.Clicked += async (sender, args) =>
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                   // Directory = "Sample",
                  //  Name = "test.jpg"
                });

                if (file == null)
                    return;

                await DisplayAlert("File Location", file.Path, "OK");

                PhotoImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    return stream;
                });
            };
        }
    }
}