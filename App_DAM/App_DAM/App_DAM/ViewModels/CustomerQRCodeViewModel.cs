using Newtonsoft.Json;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace App_DAM.ViewModels
{
   public class CustomerQRCodeViewModel : BaseViewModel
    {

        private string qrCode;
        public string QRCode
        {
            get { return qrCode; }
            set
            {
                qrCode = value;
                OnPropertyChanged(nameof(QRCode));

            }
        }

        private string nome;
        public string Nome
        {
            get { return nome; }
            set
            {
                nome = value;
                OnPropertyChanged(nameof(Nome));

            }
        }

        private string data;
        public string Data
        {
            get { return data; }
            set
            {
                data = value;
                OnPropertyChanged(nameof(Data));

            }
        }
        public CustomerQRCodeViewModel()
        {

        }
    }
}
