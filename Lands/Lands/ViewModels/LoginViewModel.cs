


namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Lands.Views;
    using Services;

    public class LoginViewModel : BaseViewModel
    {

        #region Services
        private ApiService services;
        #endregion
        #region Eventos

        #endregion
        #region atributos
        private string email;
        private string password;
        private bool isRunning;
        private bool isEnabled;
        #endregion
        #region Propiedades
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                SetValue(ref email, value);
            }

        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                SetValue(ref password,value);
            }
        }
        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                SetValue(ref isRunning, value);
            }
        }
        public bool IsRemembered
        {
            get;
            set;
        }
        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                SetValue(ref isEnabled, value);
            }
        }
        #endregion
        #region Comandos
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }

        }


        #endregion
        #region Constructores
        public LoginViewModel()
        {
            IsRemembered = true;
            IsEnabled = true;
            this.Email = "lorenaquint@gmail.com";
            this.Password = "Admin2.0";
            this.services = new ApiService();
        }


        #endregion
        #region Métodos
        private async void Login()
        {
            
            if (string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Debes ingresar el email",
                        "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Debes ingresar el password",
                        "Aceptar");
                return;
            }
            this.IsRunning = true;
            this.IsEnabled = false;
            //if (this.Email != "clquintero79@misena.edu.co" || this.Password != "Admin2.0")
            //{

            //    this.IsRunning = false;
            //    this.IsEnabled = true;
            //    await Application.Current.MainPage.DisplayAlert(
            //            "Error",
            //            "Las credenciales son incorrectas",
            //            "Aceptar");
            //    this.Password = string.Empty;
            //    return;
            //}

            var connection = await services.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                        "Error",
                         connection.Message,
                        "Aceptar");
                return;
            }

            var token = await this.services.GetToken(
                 "http://lonapi.azurewebsites.net",
                 this.Email,
                 this.Password);
            if (token == null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                        "Error",
                         "Ha ocurrido un error intentelo de nuevo",
                        "Aceptar");
                return;

            }
            if (string.IsNullOrEmpty(token.AccessToken))
                {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                        "Error",
                         token.ErrorDescription,
                        "Aceptar");
                this.Password = string.Empty;
                return;

            }
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            mainViewModel.Lands = new LandsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());
            this.IsRunning = false;
            this.IsEnabled = true;
            /*await Application.Current.MainPage.DisplayAlert(
                        "Ingreso",
                        "Bienvenido a mi app",
                        "Aceptar");
                        */

            this.Email = string.Empty;
            this.Password = string.Empty;
           
          

        }
        #endregion

    }
}
