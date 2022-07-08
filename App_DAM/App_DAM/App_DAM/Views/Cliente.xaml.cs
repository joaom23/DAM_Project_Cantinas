using App_DAM.Helper;
using App_DAM.Helpers;
using App_DAM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace App_DAM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cliente : ContentPage
    {
        private readonly string _userId = Preferences.Get("Id", "NULL");
        private readonly string _token = Preferences.Get("Token", "NULL");
        private HttpClient _client;

        CustomMap customMap;
        CustomPin userPin;

        public Cliente()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            _client = new HttpClient();

            Grid grid = new Grid();

            customMap = new CustomMap
            {
                MapType = MapType.Street
            };

            grid.Children.Add(customMap);
            Content = grid;

            customMap.CustomPins = new List<CustomPin>();

            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                Task.Run(async () =>
                {
                    await InitialiazeAndUpdatePinsOnMap();
                });
                return true;
            });
        }

        public async Task<Position> GetUserLocation()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();

            if (location == null)
            {
                location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Best,
                    Timeout = TimeSpan.FromSeconds(10)
                });
            };

            Position p = new Position(location.Latitude, location.Longitude);

            return p;
        }

        public async Task InitialiazeAndUpdatePinsOnMap()
        {
            if (customMap.Pins.Count != 0)
            {
                customMap.Pins.Clear();
            }

            var userPosition = await GetUserLocation();

            userPin = new CustomPin
            {
                Type = PinType.Place,
                //Position = new Position(38.707162, -9.139630),
                Position = new Position(userPosition.Latitude, userPosition.Longitude),
                Label = "Você está aqui",
                Name = "userPin"
            };

            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(userPin.Position, Distance.FromKilometers(0.2)));
            customMap.CustomPins.Add(userPin);
            customMap.Pins.Add(userPin);

            var response = await _client.GetAsync(API_IP.IP + "/api/customer/localizacaocantinas");

            var responsebody = await response.Content.ReadAsStringAsync();

            var cantinasLocalizacao = JsonConvert.DeserializeObject<PositionCantinasDto>(responsebody);

            if (cantinasLocalizacao.IsSuccess)
            {
                foreach (var item in cantinasLocalizacao.PosicaoCantinas)
                {
                    CustomPin pinCantina = new CustomPin
                    {
                        Type = PinType.Place,
                        Position = new Position(item.Latitude, item.Longitude),
                        Label = item.Nome,
                        Name = "Cantina",
                        Id = item.cantinaId.ToString()
                    };

                    customMap.CustomPins.Add(pinCantina);
                    customMap.Pins.Add(pinCantina);
                }
            }

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await InitialiazeAndUpdatePinsOnMap();
        }

        protected override bool OnBackButtonPressed() //Desativar botao de voltar atras do android
        {
            return true;
        }
    }
}