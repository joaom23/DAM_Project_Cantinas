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
    public partial class ForgotPasswordPage : ContentPage
    {
        ForgotPasswordViewModel viewModel;
        public ForgotPasswordPage()
        {
            viewModel = new ForgotPasswordViewModel();
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}