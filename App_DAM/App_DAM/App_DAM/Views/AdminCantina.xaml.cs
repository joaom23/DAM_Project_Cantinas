using App_DAM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace App_DAM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminCantina : ContentPage
    {
        AdminCantinaViewModel viewModel;
        public AdminCantina()
        {
            viewModel = new AdminCantinaViewModel();
            BindingContext = viewModel;
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var scan = new ZXingScannerPage();
            await Navigation.PushModalAsync(scan);

            scan.OnScanResult += result =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopModalAsync();
                    await DisplayAlert("Valor do qr code", "" + result.Text + "Refeição registada com sucesso", "Ok");
                });
            };

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await viewModel.GetCantinas();

        }

        protected override bool OnBackButtonPressed() //Desativar botao de voltar atras do android
        {
            return true;
        }

        private async void Logout(object sender, EventArgs e)
        {
            Preferences.Remove("Email");
            Preferences.Remove("Id");
            Preferences.Remove("Token");
            await App.Current.MainPage.Navigation.PushAsync(new MainPage());
        }
    }
}