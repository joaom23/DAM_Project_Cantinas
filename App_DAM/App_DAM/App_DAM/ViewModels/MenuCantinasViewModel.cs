using System;
using System.Collections.Generic;
using System.Text;

using System.Collections.ObjectModel;
using App_DAM.Models.Dtos;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using App_DAM.Views;
using App_DAM.Models;
using System.Windows.Input;
using ZXing.Net.Mobile.Forms;
using App_DAM.Helper;

namespace App_DAM.ViewModels
{
  
    class MenuCantinasViewModel : BaseViewModel
    {

       


        public ObservableCollection<CantinaDto> cantinas;

        public ObservableCollection<PratoDiaDto> prato_do_dia;


        private readonly HttpClient _client;


        public ObservableCollection<CantinaDto> Cantinas
        {
            get { return cantinas; }
            set
            {
                cantinas = value;
                OnPropertyChanged(nameof(Cantinas));
            }
        }


        public ObservableCollection<PratoDiaDto> Prato_do_dia
        {
            get { return prato_do_dia; }
            set
            {
                prato_do_dia = value;
                OnPropertyChanged(nameof(Cantinas));
            }
        }

    
      

 



        public MenuCantinasViewModel()
        {
            cantinas = new ObservableCollection<CantinaDto>();

            prato_do_dia = new ObservableCollection<PratoDiaDto>();

            _client = new HttpClient();




        }

        ZXingBarcodeImageView barcode; 

        public async Task ChooseCantinaAsync(object sender, EventArgs e, int id)
        {
            bool answer = await App.Current.MainPage.DisplayAlert("Reserva", "Pretende reservar a refeiçáo ?", "Sim", "Não");
          
        
          
            if (answer.ToString() == "true")
            {
                //gerar qr code

                barcode = new ZXingBarcodeImageView
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    AutomationId = "zxingBarcodeImageView",
                };
                barcode.BarcodeFormat = ZXing.BarcodeFormat.QR_CODE;
                barcode.BarcodeValue = id.ToString() ; // + id prato dia

                //evocar metodo da api para guardar o qr code, falta adicionar ao valor o id do prato do dia , ver NAO ACEDE AO SERVIÇO (1)

            }
            else
            {
                //nada acontece
            }
          
        }



        public async Task ListaCantinas()
        {
            this.cantinas.Clear();
       
            //var responsee = await _client.GetAsync("http://192.168.1.83:5000" + "/api/Customer/Cantinas");
            var responsee = await _client.GetAsync(API_IP.IP + "/api/Customer/Cantinas");

            var responsebody = await responsee.Content.ReadAsStringAsync();

            var cantinas = JsonConvert.DeserializeObject<ReturnCantinasDto>(responsebody);

            foreach (var c in cantinas.Cantinas)
            {
                
                var cantina = new CantinaDto
                {               
                    Morada = c.Morada,
                    HoraAbertura =  c.HoraAbertura,
                    HoraFecho = c.HoraFecho,
                    IdCantina = c.IdCantina
                };

                this.cantinas.Add(cantina);


                //-----------------------NAO ACEDE AO SERVIÇO (1)
                //--------------cria criar lista com nome da cantina, horas de abertura e fecho , prato do dia e preco 
                //mas nao consigo aceder ao metodo para ir buscar o prato do dia e apresentar na view
                
                var responsee2 = await _client.GetAsync("http://192.168.1.83:5000" + "/api/AdminCantina/PratoDia/" + c.IdCantina + "/" + DateTime.Now);

                var responsebody2 = await responsee2.Content.ReadAsStringAsync();

                var PratoDia = JsonConvert.DeserializeObject<PratoDiaDto>(responsebody2);

             

            }

            //pratos do dia 
            this.prato_do_dia.Clear();
        }
    }
}

