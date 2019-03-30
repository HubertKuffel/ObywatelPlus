using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ObywatelPlus
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Cat : ContentPage
	{
		public Cat()
		{
			InitializeComponent();
		}

        private void WyslijButton_Clicked(object sender, EventArgs e)
        {
            var client = new TcpClient();
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("149.56.102.192"), 5678);
            try
            {
                client.Connect(ipEndPoint);
                if (client.Connected)
                {
                    var reader = new StreamReader(client.GetStream());
                    var writer = new StreamWriter(client.GetStream());
                    writer.AutoFlush = true;

                    if (client.Connected)
                    {
                        writer.WriteLine("kotek");
                    }
                }
                client.Dispose();
            }
            catch (Exception ex)
            {
            }
        }
    }
}