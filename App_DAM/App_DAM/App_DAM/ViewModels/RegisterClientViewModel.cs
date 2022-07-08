using App_DAM.Models;
using App_DAM.Models.Dtos;
using App_DAM.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App_DAM.ViewModels
{
    class RegisterClientViewModel : BaseViewModel
    {
     
        private static bool isDebug = false;

        private string email;
        public string Email
        {
            get { return email; }
            set
            {

                email = value;
                OnPropertyChanged(nameof(Email));
                RegisterCommand?.ChangeCanExecute();
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {

                password = value;
                OnPropertyChanged(nameof(Password));
                RegisterCommand?.ChangeCanExecute();
            }
        }

        private string confirmpassword;
        public string ConfirmPassword
        {
            get { return confirmpassword; }
            set
            {

                confirmpassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
                RegisterCommand?.ChangeCanExecute();
            }
        }
    
        public Command RegisterCommand { get; }
        private readonly HttpClient _client;

        public RegisterClientViewModel()
        {
            _client = new HttpClient();
            RegisterCommand = new Command(async () => { await Regist(); }, CanExecuteLogin);

        }

        public async Task Regist()
        {
            RegisterDto client = new RegisterDto();

            if (isDebug)
            {

                client.Email = "teste2@gmail.com";
                client.Password = "teste2.123";
                client.ConfirmPassword = "teste2.123";
            }
            else
            {
                client.Email = Email;
                client.Password = Password;
                client.ConfirmPassword = ConfirmPassword;
            }

            /*
            var data = new StringContent(
                    JsonConvert.SerializeObject(client, Newtonsoft.Json.Formatting.Indented),
                    Encoding.UTF8,
                    "application/json");

            var response = await _client.PostAsync("http://localhost:5609/api/Users/RegisterUser", data);
            */
            //  var responsebody = await response.Content.ReadAsStringAsync();

            // var responseObject = JsonConvert.DeserializeObject<Utilizador>(responsebody);


            //   var data = new StringContent(
            //        JsonConvert.SerializeObject(client, Newtonsoft.Json.Formatting.Indented),
            //        Encoding.UTF8,
            //        "application/json");

            if (client.Email.Contains("cantina"))
            {
                var formContent = new MultipartFormDataContent
                {
                     { new StringContent(client.Email), "Email" },
                     { new StringContent(client.Password), "Password" },
                     { new StringContent(client.ConfirmPassword), "ConfirmPassword" },
                { new StringContent("AdminCantina"), "Role" }


                };



                var response = await _client.PostAsync("http://192.168.1.83:5000" + "/api/users/RegisterUser", formContent);

                if (response.IsSuccessStatusCode)
                {
                    await App.Current.MainPage.DisplayAlert("Registo", "Registo efetuado com sucesso", "Ok");

                    await App.Current.MainPage.Navigation.PushAsync(new LoginPage());

                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Registo", "Erro", "Ok");
                    Password = "";
                    ConfirmPassword = "";
                }

            }else{


                var formContent = new MultipartFormDataContent
                {
                     { new StringContent(client.Email), "Email" },
                     { new StringContent(client.Password), "Password" },
                     { new StringContent(client.ConfirmPassword), "ConfirmPassword" },
                { new StringContent("Cliente"), "Role" }


                };



                var response = await _client.PostAsync("http://192.168.1.83:5000" + "/api/users/RegisterUser", formContent);

                if (response.IsSuccessStatusCode)
                {
                    await App.Current.MainPage.DisplayAlert("Registo", "Registo efetuado com sucesso", "Ok");

                    await App.Current.MainPage.Navigation.PushAsync(new LoginPage());

                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Registo", "Erro", "Ok");
                    Password = "";
                    ConfirmPassword = "";
                }


            }
               


           
        }

        private bool CanExecuteLogin()
        {
            if (isDebug)
            {
                return true;
            }

            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(ConfirmPassword))
            {
                return false;
            }

            return true;
        }

    }
}
