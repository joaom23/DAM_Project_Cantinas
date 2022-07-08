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
    public partial class ListaCantinas : ContentPage
    {
        ListaCantinasViewModel viewModel;
        public ListaCantinas()
        {
            viewModel = new ListaCantinasViewModel();
            BindingContext = viewModel;
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await viewModel.GetCantinas();

        }
    }
}