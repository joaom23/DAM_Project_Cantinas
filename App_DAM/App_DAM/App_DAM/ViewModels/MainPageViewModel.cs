using App_DAM.Helper;
using App_DAM.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace App_DAM.ViewModels
{
    class MainPageViewModel : BaseViewModel
    {
        public Command LoginPageCommnad { get; }
        public Command RegistoPageCommnad { get; }

        private ImageSource imagem = API_IP.IP + "/api/users/getimage/imagensapp/cantina.png";

        public ImageSource Imagem
        {
            get { return imagem; }
            set
            {
                imagem = value;
                OnPropertyChanged(nameof(Imagem));
            }
        }

        public MainPageViewModel()
        {
            LoginPageCommnad = new Command(LoginPage);
            RegistoPageCommnad = new Command(RegistoPage);
        }

        private async void LoginPage()
        {
            await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }

        private async void RegistoPage()
        {
            await App.Current.MainPage.Navigation.PushAsync(new RegisterClientPage());
        }
    }
}
