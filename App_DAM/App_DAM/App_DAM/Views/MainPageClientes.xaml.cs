using App_DAM.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App_DAM
{
    [DesignTimeVisible(false)]
    [Obsolete]
    public partial class MainPageClientes : MasterDetailPage
    {
        public MainPageClientes()
        {
            InitializeComponent();
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            this.Master = new Views.MenuCliente();
            this.Detail = new NavigationPage(new Cliente());
        }

        protected override bool OnBackButtonPressed() //Desativar botao de voltar atras do android
        {
            return true;
        }
    }
}
