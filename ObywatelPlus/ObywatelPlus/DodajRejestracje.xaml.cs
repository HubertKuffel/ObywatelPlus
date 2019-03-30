using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ObywatelPlus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DodajRejestracje : ContentPage
    {
        public DodajRejestracje()
        {
            InitializeComponent();
            ((ListView)listRejestracji).ItemsSource = RejestracjaChecker.Rejestracje;
        }

        private void Button_Clicked4(object sender, EventArgs e)
        {
            var rejestracja = this.rejestacjaText1.Text;
            if (!string.IsNullOrEmpty(rejestracja))
            {
                this.rejestacjaText1.Text = "";
                RejestracjaChecker.Rejestracje.Add(rejestracja);
                ((ListView)listRejestracji).BeginRefresh();
                ((ListView)listRejestracji).ItemsSource = null;
                ((ListView)listRejestracji).ItemsSource = RejestracjaChecker.Rejestracje;
                ((ListView)listRejestracji).EndRefresh();
            }
        }

    }
}