﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ObywatelPlus
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Hubert : ContentPage
	{
		public Hubert ()
		{
			InitializeComponent ();
		}

        private async void Button_Clicked1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DodajRejestracje());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var rejestracja = this.rejestacjaText1.Text;
            if (!string.IsNullOrEmpty(rejestracja))
            {
                RejestracjaChecker.Alarm(rejestracja);
            }
        }
    }
}