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
    public partial class QRCodesPage : ContentPage
    {
        QRCodesViewModel viewModel;
        public QRCodesPage()
        {
            viewModel = new QRCodesViewModel();
            BindingContext = viewModel;
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await viewModel.GetQRCodes();

        }
    }
}