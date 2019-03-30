using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace ObywatelPlus
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            XF.Material.Forms.Material.Init(this);

            MainPage = new NavigationPage(new ObywatelPlus.MainPage());
            Thread thread = new Thread(RejestracjaChecker.CheckerLoop);
            thread.IsBackground = true;
            thread.Start();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
