

namespace Lands.ViewModels
{
    public class MainViewModel
    {
        #region ViewModel
        public LoginViewModel Login
        {
            get;
            set;
        }
		public PaisViewModel Pais
        {
            get;
            set;
        }
        public LandsViewModel Lands
        {
            get;
            set;
        }

        #endregion

        #region Constructores
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
        }
        #endregion
        //7Una unica ven viewmodel
        #region Sinlgeton
        public static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            else
            {
                return instance;
            }

        }
        #endregion
    }
}
