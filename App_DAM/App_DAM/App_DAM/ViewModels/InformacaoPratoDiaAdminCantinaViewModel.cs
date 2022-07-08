using App_DAM.Helper;
using App_DAM.Models;
using App_DAM.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App_DAM.ViewModels
{
    class InformacaoPratoDiaAdminCantinaViewModel : BaseViewModel
    {
        private readonly HttpClient _client;

        private int pratoDiaId;
        public int PratoDiaId
        {
            get { return pratoDiaId; }
            set
            {

                pratoDiaId = value;
                OnPropertyChanged(nameof(PratoDiaId));
            }
        }

        private string nomeCantina;
        public string NomeCantina
        {
            get { return nomeCantina; }
            set
            {

                nomeCantina = value;
                OnPropertyChanged(nameof(NomeCantina));
            }
        }

        private string data;
        public string Data
        {
            get { return data; }
            set
            {

                data = value;
                OnPropertyChanged(nameof(Data));
            }
        }

        private string refeicoesMarcadas;
        public string RefeicoesMarcadas
        {
            get { return refeicoesMarcadas; }
            set
            {

                refeicoesMarcadas = value;
                OnPropertyChanged(nameof(RefeicoesMarcadas));
            }
        }

        private string refeicoesConsumidas;
        public string RefeicoesConsumidas
        {
            get { return refeicoesConsumidas; }
            set
            {

                refeicoesConsumidas = value;
                OnPropertyChanged(nameof(RefeicoesConsumidas));
            }
        }
        public InformacaoPratoDiaAdminCantinaViewModel()
        {
            _client = new HttpClient();
        }

        public async Task VerificarQRCode(string QRCode, CantinasInformation c)
        {
            var formContent = new MultipartFormDataContent
                {
                     { new StringContent(QRCode), "QRCode" },
                     { new StringContent(PratoDiaId.ToString()), "PratoDiaId" }
                };

            var response = await _client.PostAsync(API_IP.IP + "/api/admincantina/validarqrcode", formContent);
            var responsebody = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<Response>(responsebody);

            if (responseObject.IsSuccess)
            {
                c.RefeicoesConsumidas = responseObject.Update;

                await App.Current.MainPage.Navigation.PushAsync(new InformacaoPratoDiaAdminCantina(c));
                await App.Current.MainPage.DisplayAlert("Aviso", responseObject.Message, "Ok");
            }
            else
            {
                
                await App.Current.MainPage.Navigation.PushAsync(new InformacaoPratoDiaAdminCantina(c));
                await App.Current.MainPage.DisplayAlert("ERRO", responseObject.Message, "Ok");
            }
        }
    }
}

