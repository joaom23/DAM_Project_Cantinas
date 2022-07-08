using App_DAM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_DAM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel viewModel;
        //iisexpress-proxy localhost:56069 to 5000
        public LoginPage()
        {
            InitializeComponent();
            //  this.BindingContext = new LoginViewModel();
            viewModel = new LoginViewModel();
            BindingContext = viewModel;

        }
    }
}