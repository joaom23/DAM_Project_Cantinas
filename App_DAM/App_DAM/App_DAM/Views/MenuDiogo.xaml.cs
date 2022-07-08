using App_DAM.Models.Dtos;
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
    public partial class MenuDiogo : ContentPage
    {
        MenuCantinasViewModel viewModel;

        public MenuDiogo()
        {
            InitializeComponent();


            viewModel = new MenuCantinasViewModel();
            BindingContext = viewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await viewModel.ListaCantinas();
           
           

        }

     

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
         
            var id = (e.Item as CantinaDto).IdCantina;

            viewModel.ChooseCantinaAsync(sender,e,id);
        }
    }

}