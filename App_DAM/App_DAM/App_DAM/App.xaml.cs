using App_DAM.Services;
using App_DAM.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_DAM
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new MainPage());
            //MainPage = new NavigationPage(new CantinaInformation(2));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
