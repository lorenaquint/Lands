


namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;


    public class LoginViewModel : BaseViewModel
    {
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
            if (this.Email != "clquintero79@misena.edu.co" || this.Password != "Admin2.0")
            {

                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Las credenciales son incorrectas",
                        "Aceptar");
                this.Password = string.Empty;
                return;
            }
            this.IsRunning = false;
            this.IsEnabled = true;
            await Application.Current.MainPage.DisplayAlert(
                        "Ingreso",
                        "Bienvenido a mi app",
                        "Aceptar");
            this.Email = string.Empty;
            this.Password = string.Empty;
            return;

        }
        #endregion

    }
}
