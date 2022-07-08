using App_DAM.Helper;
using App_DAM.Models;
using App_DAM.Models.Dtos;
using App_DAM.Models.Informations;
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

namespace App_DAM.ViewModels
{
    class QRCodesViewModel : BaseViewModel
    {
        private readonly HttpClient _client;
        private readonly string _userId = Preferences.Get("Id", "NULL");
        private readonly string _token = Preferences.Get("Token", "NULL");

        public ObservableCollection<QRCodesInformation> qrCodes;

        public ObservableCollection<QRCodesInformation> QRCodes
        {
            get { return qrCodes; }
            set
            {
                qrCodes = value;
                OnPropertyChanged(nameof(QRCodes));
            }
        }

        public Command<QRCodesInformation> ShowQRCodeCommand { get; }
        public QRCodesViewModel()
        {
            ShowQRCodeCommand = new Command<QRCodesInformation>((obj) => ShowQRCode(obj));
            _client = new HttpClient();
            qrCodes = new ObservableCollection<QRCodesInformation>();
        }

        public async Task GetQRCodes()
        {
            qrCodes.Clear();

            var responsee = await _client.GetAsync(API_IP.IP + "/api/customer/qrcodes/" +_userId);

            var responsebody = await responsee.Content.ReadAsStringAsync();

            var qrCodesReturned = JsonConvert.DeserializeObject<ReturnQRCodesDto>(responsebody);

            foreach (var qr in qrCodesReturned.QRCodesInformation)
            {
                var qrCodeParaLista = new QRCodesInformation 
                {
                    NomeCantina = qr.NomeCantina,
                    Data = qr.Data,
                    QRCode = qr.QRCode
                };

                qrCodes.Add(qrCodeParaLista);
            }
        }
        
        public async void ShowQRCode(QRCodesInformation qrCode)
        {
            await App.Current.MainPage.Navigation.PushAsync(new VerQRCode(qrCode));
        }
    }
}
