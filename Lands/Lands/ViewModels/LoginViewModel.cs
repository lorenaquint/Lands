


namespace Lands.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    public class LoginViewModel
    {
        #region Propiedades
        public string  Email
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public bool IsRunning
        {
            get;
            set;
        }
        public bool IsRemembered
        {
            get;
            set;
        }
        #endregion
        #region Comandos
          public ICommand LoginCommand
        {
            get;
            //{
            //    //return RelayCommand;
            //}
            
        }
        #endregion
        #region Constructores
        public LoginViewModel()
        {
            IsRemembered = true;
        }
        #endregion
        #region Métodos
       
        #endregion

    }
}
