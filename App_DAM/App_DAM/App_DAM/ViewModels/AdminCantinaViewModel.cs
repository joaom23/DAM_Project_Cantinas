using App_DAM.Helper;
using App_DAM.Models;
using App_DAM.Models.Dtos;
using App_DAM.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;

namespace App_DAM.ViewModels
{
    class AdminCantinaViewModel : BaseViewModel
    {
        private readonly HttpClient _client;
        private readonly string _userId = Preferences.Get("Id", "NULL");
        private readonly string _token = Preferences.Get("Token", "NULL");
        private int _IdPratoDia;
        public ObservableCollection<CantinasInformation> cantinas;

        public ObservableCollection<CantinasInformation> Cantinas
        {
            get { return cantinas; }
            set
            {
                cantinas = value;
                OnPropertyChanged(nameof(Cantinas));
            }
        }

        public Command<CantinasInformation> GetCantinaInfoCommand { get; }
        public AdminCantinaViewModel()
        {
            GetCantinaInfoCommand = new Command<CantinasInformation>((obj) => GetCantinasInfo(obj));
            _client = new HttpClient();
            cantinas = new ObservableCollection<CantinasInformation>();
        }

        public async Task GetCantinas()
        {
            cantinas.Clear();

            var responsee = await _client.GetAsync(API_IP.IP + "/api/customer/cantinas");

            var responsebody = await responsee.Content.ReadAsStringAsync();

            var cantinasReturnerd = JsonConvert.DeserializeObject<ReturnCantinasDto>(responsebody);

            foreach (var c in cantinasReturnerd.Cantinas)
            {
                var cantinaParaLista = new CantinasInformation 
                {
                    Id = c.IdCantina,
                    Nome = c.Nome,
                    Imagem = API_IP.IP + "/api/users/getimage/cantinas/" + c.Foto
                };

                var dataHoje = DateTime.Now.ToShortDateString();

                foreach (var item in c.PratosDia)
                {
                    var auxData = item.Data.ToShortDateString();
                    if(auxData == dataHoje)
                    {
                        cantinaParaLista.IdPratoDia = item.IdPratoDia;
                        cantinaParaLista.RefeicoesMarcadas = item.RefeicoesMarcadas;
                        cantinaParaLista.RefeicoesConsumidas = item.RefeicoesConsumidas;
                    }
                }

                cantinas.Add(cantinaParaLista);
            }
        }
        
        public async void GetCantinasInfo(CantinasInformation cantina)
        {
            await App.Current.MainPage.Navigation.PushAsync(new InformacaoPratoDiaAdminCantina(cantina));
        }
    }
}
