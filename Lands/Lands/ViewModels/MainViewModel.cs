


namespace Lands.ViewModels
{

using System.Collections.Generic;
	using Lands.Models;

	public class MainViewModel
    {
		#region Propiedades
		public List<Land> LandsList
        {
            get;
            set;
        }
		#endregion
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
