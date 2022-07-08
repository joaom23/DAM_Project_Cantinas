using App_DAM.Helper;
using App_DAM.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App_DAM.ViewModels
{
    public class MenuClienteViewModel : BaseViewModel
    {
        private readonly string _fotoUser = Preferences.Get("Foto", "NULL");
        private readonly HttpClient _client;
        private readonly string _userId = Preferences.Get("Id", "NULL");
        private readonly string _token = Preferences.Get("Token", "NULL");

        private string info;
        public string Info
        {
            get { return info; }
            set
            {

                info = value;
                OnPropertyChanged(nameof(Info));
            }
        }

        private string information;
        public string Information
        {
            get { return information; }
            set
            {

                information = value;
                OnPropertyChanged(nameof(Information));

            }
        }

        private ImageSource fotoUser;
        public ImageSource FotoUser
        {
            get { return fotoUser; }
            set
            {
                fotoUser = value;
                OnPropertyChanged(nameof(FotoUser));
            }
        }

        private ImageSource banner;
        public ImageSource Banner
        {
            get { return banner; }
            set
            {
                banner = value;
                OnPropertyChanged(nameof(Banner));
            }
        }
        public Command LogoutCommand { get; }
        public Command CantinasCommand { get; }
        public Command QRCodesPageCommand { get; }
        public MenuClienteViewModel()
        {
            Preferences.Set("doAgain", "true");
            CantinasCommand = new Command(Cantinas);
            LogoutCommand = new Command(Logout);
            QRCodesPageCommand = new Command(QRCodes);
            //AddInformationCommnad = new Command(async () => { await AddInformation(); }, CanExecuteAddInformation);
            //ListaVendidosPageCommand = new Command(ListaVendidosPage);
            FotoUser = ImageSource.FromUri(new Uri(API_IP.IP + "/api/users/getimage/imagensapp/user.png"));
            _client = new HttpClient();
        }

        public async void QRCodes()
        {
            await App.Current.MainPage.Navigation.PushAsync(new QRCodesPage());
        }

        public async void Cantinas()
        {
            await App.Current.MainPage.Navigation.PushAsync(new ListaCantinas());
        }

        public async void Logout()
        {
            Preferences.Remove("Id");
            Preferences.Remove("Token");
            await App.Current.MainPage.Navigation.PushAsync(new MainPage());
        }
    }
}
