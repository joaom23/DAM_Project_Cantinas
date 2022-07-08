using App_DAM.Models.Informations;
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
    public partial class VerQRCode : ContentPage
    {
        CustomerQRCodeViewModel viewModel;
        private string _QRCode;
        private string _Data;
        private string _Nome;
        public VerQRCode(QRCodesInformation QRCode)
        {
            _QRCode = QRCode.QRCode;
            _Data = QRCode.Data;
            _Nome = QRCode.NomeCantina;
            viewModel = new CustomerQRCodeViewModel();
            BindingContext = viewModel;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.QRCode = _QRCode;
            viewModel.Data = _Data;
            viewModel.Nome = _Nome;
        }
    }
}