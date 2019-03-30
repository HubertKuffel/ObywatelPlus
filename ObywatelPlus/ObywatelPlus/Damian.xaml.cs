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
using System.Net;
using System.Net.Sockets;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Security.Cryptography;

namespace ObywatelPlus
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Damian : ContentPage
	{
		public Damian ()
		{
			InitializeComponent ();
            byte[] imgByte = new byte[] { };
            MediaFile file = null;

            CameraButton.Clicked += async (sender, args) =>
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                   // Directory = "Sample",
                  //  Name = "test.jpg"
                });

                if (file == null)
                    return;

                PhotoImage.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            };

            WyslijButton.Clicked += async (sender, args) =>
            {
                string description = Opis.Text;
                string coordinates = "Latitude:53.12817|Longitude: 18.020475";

                byte[] b;

                using (BinaryReader br = new BinaryReader(file.GetStream()))
                {
                    b = br.ReadBytes((int)file.GetStream().Length);
                }

                var base64 = Convert.ToBase64String(b);

                var message = "new_des;" + base64 + ";" + coordinates + ";" + description + ";";
                var client = new TcpClient();
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("149.56.102.192"), 5678);
                try
                {
                    client.Connect(ipEndPoint);
                    if (client.Connected)
                    {
                        var readerr = new StreamReader(client.GetStream());
                        var writer = new StreamWriter(client.GetStream());
                        writer.AutoFlush = true;

                        if (client.Connected)
                        {
                            writer.WriteLine(message);
                        }
                        var result = bool.Parse(readerr.ReadLine());
                        if (result)
                        {
                            await DisplayAlert("Dziękujemy za zgłoszenie","", "OK");
                        }
                    }
                    client.Dispose();
                }
                catch (Exception e)
                {
                }

            };
        }
    }
}