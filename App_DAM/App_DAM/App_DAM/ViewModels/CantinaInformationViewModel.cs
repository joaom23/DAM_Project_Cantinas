using App_DAM.Helper;
using App_DAM.Models;
using App_DAM.Models.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App_DAM.ViewModels
{
    class CantinaInformationViewModel : BaseViewModel
    {
        private readonly HttpClient _client;
        private readonly string _userId = Preferences.Get("Id", "NULL");
        private readonly string _token = Preferences.Get("Token", "NULL");
        private bool First { get; set; } = true;
        private int _IdCantina;
        private int _IdPratoDia;
        private string _data;

        private DateTime dataEscolhida;
        public DateTime DataEscolhida
        {
            get { return dataEscolhida; }
            set
            {
                if (First)
                {
                    dataEscolhida = DateTime.Now;
                }
                else
                {
                    dataEscolhida = value;
                }

                OnPropertyChanged(nameof(DataEscolhida));

                if (!First)
                {
                    AtualizarData();
                }
                else
                {
                    First = false;
                }
                //if(dataEscolhida != null)
                //{
                //    dataEscolhida = value;
                //    OnPropertyChanged(nameof(DataEscolhida));
                //    AtualizarData();
                //}
                
            }
        }

        private DateTime hoje;
        public DateTime Hoje
        {
            get { return hoje; }
            set
            {
                hoje = value;
                OnPropertyChanged(nameof(Hoje));
                //AtualizarData();
            }
        }

        private bool verBotaoRemoverReserva;
        public bool VerBotaoRemoverReserva
        {
            get { return verBotaoRemoverReserva; }
            set
            {
                verBotaoRemoverReserva = value;
                OnPropertyChanged(nameof(VerBotaoRemoverReserva));
            }
        }

        private bool verBotaoReserva;
        public bool VerBotaoReserva
        {
            get { return verBotaoReserva; }
            set
            {
                verBotaoReserva = value;
                OnPropertyChanged(nameof(VerBotaoReserva));
            }
        }

        private string titulo;
        public string Titulo
        {
            get { return titulo; }

            set
            {
                titulo = value;
                OnPropertyChanged(nameof(Titulo));
            }
        }

        private string mensagem;
        public string Mensagem
        {
            get { return mensagem; }

            set
            {
                mensagem = value;
                OnPropertyChanged(nameof(Mensagem));
            }
        }

        private int id;
        public int Id
        {
            get { return id; }

            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string preco;
        public string Preco
        {
            get { return preco; }

            set
            {
                preco = value;
                OnPropertyChanged(nameof(Preco));
            }
        }

        private string nome;
        public string Nome
        {
            get { return nome; }

            set
            {
                nome = value;
                OnPropertyChanged(nameof(Nome));
            }
        }

        private ImageSource imagem;
        public ImageSource Imagem
        {
            get { return imagem; }

            set
            {
                imagem = value;
                OnPropertyChanged(nameof(Imagem));
            }
        }

        public ObservableCollection<PratoDiaInformation> pratos;
        public ObservableCollection<PratoDiaInformation> Pratos
        {
            get { return pratos; }
            set
            {
                pratos = value;
                OnPropertyChanged(nameof(Pratos));
            }
        }
        public Command FazerReservaCommand { get; }
        public Command RemoverReservaCommand { get; }
        public CantinaInformationViewModel()
        {
            _client = new HttpClient();
            pratos = new ObservableCollection<PratoDiaInformation>();
            FazerReservaCommand = new Command(FazerReserva);
            RemoverReservaCommand = new Command(RemoverReserva);
        }

        public async Task MostrarPratoDia(int IdCantina)
        {
            _IdCantina = IdCantina;
            pratos.Clear();
            var data = DataEscolhida;
            var dia = data.Day;
            var mes = data.Month;
            var ano = data.Year;

            var dataEnviar = ano + "-" + mes + "-" + dia;
            _data = dataEnviar;

            var response = await _client.GetAsync(API_IP.IP + "/api/admincantina/pratodia/" + IdCantina + "/" + dataEnviar);

            var responsebody = await response.Content.ReadAsStringAsync();

            var pratoDia = JsonConvert.DeserializeObject<PratoDiaDto>(responsebody);

            Titulo = "Pratos do dia - Cantina " + pratoDia.Cantina.Nome;

            //Se nao existir prato para determinado dia
            if(pratoDia.PratoId == 0) 
            {
                Mensagem = "Não existe prato defenido.";
                Id = 0;
                Nome = "";
                Imagem = "";
                Preco = "";

                VerBotaoReserva = false;
                VerBotaoRemoverReserva = false;
            }
            else
            {
                Mensagem = "";
                Id = pratoDia.PratoId;
                Nome = pratoDia.Prato.Descricao;
                Imagem = API_IP.IP + "/api/users/getimage/pratos/" + pratoDia.Prato.Foto;
                Preco = pratoDia.Prato.Preco.ToString() + " €";

                // Verificar se é para desativar botao
                var formContent = new MultipartFormDataContent
                {
                     { new StringContent(_userId), "ClienteId" },
                     { new StringContent(pratoDia.IdPratoDia.ToString()), "PratoDiaId" }
                };

                var response2 = await _client.PostAsync(API_IP.IP + "/api/customer/VerificarReserva", formContent);
                var responsebody2 = await response2.Content.ReadAsStringAsync();

                var verificar = JsonConvert.DeserializeObject<VerificarReservaDto>(responsebody2);

                //Se tem reserva esconde botao
                VerBotaoReserva = !verificar.TemReserva;
                VerBotaoRemoverReserva = !VerBotaoReserva;
                _IdPratoDia = pratoDia.IdPratoDia;
            }

            Hoje = DateTime.Now;
           
        }

        public async void FazerReserva()
        {
            var QRCode = _IdPratoDia.ToString() + "_" + _userId + "_" + _data;
            var formContent = new MultipartFormDataContent
                {
                     { new StringContent(_userId), "CustomerId" },
                     { new StringContent(_IdPratoDia.ToString()), "PratoDiaId" },
                     { new StringContent(QRCode), "QRCode" },
                     { new StringContent(_data), "Data" },
                };

            var response = await _client.PostAsync(API_IP.IP + "/api/customer/marcarrefeicao", formContent);
            var responsebody = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<Response>(responsebody);

            await App.Current.MainPage.DisplayAlert("Mensagem", responseObject.Message, "Ok");

            VerBotaoReserva = false;
            VerBotaoRemoverReserva = !VerBotaoReserva;
        }

        public async void RemoverReserva()
        {
            var formContent = new MultipartFormDataContent
                {
                     { new StringContent(_userId), "CustomerId" },
                     { new StringContent(_IdPratoDia.ToString()), "PratoDiaId" },
                };

            var response = await _client.PostAsync(API_IP.IP + "/api/customer/anularrefeicao", formContent);
            var responsebody = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<Response>(responsebody);

            await App.Current.MainPage.DisplayAlert("Mensagem", responseObject.Message, "Ok");

            VerBotaoReserva = true;
            VerBotaoRemoverReserva = !VerBotaoReserva;
        }

        async void AtualizarData()
        {
            await MostrarPratoDia(_IdCantina);
        }
    }
}
