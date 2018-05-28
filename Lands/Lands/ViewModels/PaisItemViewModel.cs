
namespace Lands.ViewModels
{
	using System;
	using System.Windows.Input;
	using GalaSoft.MvvmLight.Command;
	using Models;
	using Xamarin.Forms;
	using Views;

	public class PaisItemViewModel:Land
    {
		#region Commands
        public ICommand SelectPaisCommand
        {
            get
            {
                return new RelayCommand(SelectPais);
            }
        }
		#endregion
		public PaisItemViewModel()
		{

		}
		
        
        #region Métodos

		private async void SelectPais()
		{
			MainViewModel.GetInstance().Pais = new PaisViewModel(this);
			await Application.Current.MainPage.Navigation.PushAsync(new PaisTabbedPage());

		}
		#endregion	
    }
}
