using App_DAM.Helper;
using App_DAM.Models;
using App_DAM.Models.Dtos;
using App_DAM.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App_DAM.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        private static bool isDebug = false;

        public Command LoginCommand { get; }

        private readonly HttpClient _client;
        public Command ForgotPasswordPageCommand { get; }


        private string username;
        public string Username
        {
            get { return username; }
            set
            {

                username = value;
                OnPropertyChanged(nameof(Username));
                LoginCommand?.ChangeCanExecute();
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
                LoginCommand?.ChangeCanExecute();
            }
        }

        public LoginViewModel()
        {
            _client = new HttpClient();
            LoginCommand = new Command(async () => { await Login(); }, CanExecuteLogin);
            ForgotPasswordPageCommand = new Command(ForgotPasswordPage);
        }

        private void ForgotPasswordPage()
        {
            App.Current.MainPage.Navigation.PushAsync(new ForgotPasswordPage());
        }

        private bool CanExecuteLogin()
        {
            if (isDebug)
            {
                return true;
            }

            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Username))
            {
                return false;
            }

            return true;
        }

        public async Task Login()
        {
            LoginDto user = new LoginDto();

            if (isDebug)
            {
                //user.Email = "admincantina@gmail.com";
                user.Email = "teste@gmail.com";
                user.Password = "12345";
            }
            else
            {
                user.Email = Username;
                user.Password = Password;
            }

            var formContent = new MultipartFormDataContent
                {
                     { new StringContent(user.Email), "Email" },
                     { new StringContent(user.Password), "Password" }
                };

            //var response = await _client.PostAsync("http://192.168.1.83:5000" + "/api/Users/Login", formContent);
            var response = await _client.PostAsync(API_IP.IP + "/api/users/Login", formContent);
            var responsebody = await response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<Response>(responsebody);

            if (responseObject.IsSuccess)
            {
                var token = responseObject.Message;

                var readToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

                var userId = readToken.Claims.FirstOrDefault(x => x.Type == "Id").Value;

                var userRole = readToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;

                Preferences.Set("Id", userId);
                Preferences.Set("Token", responseObject.Message);

                if (userRole == "AdminCantina")
                {
                    //await App.Current.MainPage.DisplayAlert("Login", "Login efetuado com sucesso", "Ok");

                    //Se o login for feito com sucesso vai para a pagina do admin da cantina de leitura do qrcode

                    await App.Current.MainPage.Navigation.PushAsync(new AdminCantina());
                }
                else if(userRole == "Cliente")
                {
                    //await App.Current.MainPage.DisplayAlert("Login", "Login efetuado com sucesso", "Ok");

                    //Se o login for feito com sucesso vai para a pagina da cantina

                    await App.Current.MainPage.Navigation.PushAsync(new MainPageClientes());
                }      
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Erro", responseObject.Message, "Ok");
                Password = "";
            }
        }
    }
}
