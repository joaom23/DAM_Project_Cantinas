using App_DAM.Models;
using App_DAM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace App_DAM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InformacaoPratoDiaAdminCantina : ContentPage
    {
        InformacaoPratoDiaAdminCantinaViewModel viewModel;
        private readonly CantinasInformation _cantina;
        public InformacaoPratoDiaAdminCantina(CantinasInformation c)
        {
            _cantina = c;
            viewModel = new InformacaoPratoDiaAdminCantinaViewModel();
            BindingContext = viewModel;
            InitializeComponent();
        }

        private async void LerQRCode(object sender, EventArgs e)
        {
            var scan = new ZXingScannerPage();
            await Navigation.PushAsync(scan);

            scan.OnScanResult += result =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    //await Navigation.PopModalAsync();
                    //await DisplayAlert("Valor do qr code", "" + result.Text + "Refeição registada com sucesso", "Ok");

                    // Verificar se o codigo é valido
                    await viewModel.VerificarQRCode(result.ToString(), _cantina);
                  
                });
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.PratoDiaId = _cantina.IdPratoDia;
            viewModel.RefeicoesMarcadas = "Refeições marcadas: " + _cantina.RefeicoesMarcadas.ToString();
            viewModel.RefeicoesConsumidas = "Refeições consumidas: " + _cantina.RefeicoesConsumidas;
            viewModel.NomeCantina = _cantina.Nome;
            viewModel.Data = "Informação dia " + DateTime.Now.ToShortDateString();
        }
    }
}