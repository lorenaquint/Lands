
namespace Lands.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Services;
    using Xamarin.Forms;

    public class LandsViewModel:BaseViewModel
    {
        #region Services
        private ApiService apiService;
		#endregion
		#region Atributos
        private bool isRefreshing;
		private string filter;
		private ObservableCollection<PaisItemViewModel> lands;

        #endregion
        
        #region Propiedades
		public ObservableCollection<PaisItemViewModel> Lands
        {
            get
            {
                return lands;
            }
            set
            {
                SetValue(ref lands, value);
            }

        }
        public bool IsRefreshing
        {
            get
            {
                return isRefreshing;
            }
            set
            {
                SetValue(ref isRefreshing, value);
            }
        }
        public string Filter
        {
            get
            {
                return filter;
            }
            set
            {
                SetValue(ref filter, value);
                this.Search();
            }
        }
        #endregion


        #region Constructores
        public LandsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadLands();

        }


        #endregion
        #region Métodos
        private async void LoadLands()
        {
            this.IsRefreshing = true;
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error",
                                                                connection.Message,
                                                                "Aceptar");
                await Application.Current.MainPage.Navigation.PopAsync();
                this.IsRefreshing = false;
                return;
            }
            var response = await this.apiService.GetList<Land>("https://restcountries.eu",
                                                               "/rest",
                                                               "/v2/all");
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error",
                                                                response.Message,
                                                                "Aceptar");
                this.IsRefreshing = false;
                return;
            }
            this.IsRefreshing = false;
			MainViewModel.GetInstance().LandsList = (List<Land>)response.Result;
			this.Lands = new ObservableCollection<PaisItemViewModel>(ToItemViewModel());

        }

		private IEnumerable<PaisItemViewModel> ToItemViewModel()
		{
			return MainViewModel.GetInstance().LandsList.Select(l=> new PaisItemViewModel
            {
                Alpha2Code = l.Alpha2Code,
                Alpha3Code = l.Alpha3Code,
                AltSpellings = l.AltSpellings,
                Area = l.Area,
                Borders = l.Borders,
                CallingCodes = l.CallingCodes,
                Capital = l.Capital,
                Cioc = l.Cioc,
                Currencies = l.Currencies,
                Demonym = l.Demonym,
                Flag = l.Flag,
                Gini = l.Gini,
                Languages = l.Languages,
                Latlng = l.Latlng,
                Name = l.Name,
                NativeName = l.NativeName,
                NumericCode = l.NumericCode,
                Population = l.Population,
                Region = l.Region,
                RegionalBlocs = l.RegionalBlocs,
                Subregion = l.Subregion,
                Timezones = l.Timezones,
                TopLevelDomain = l.TopLevelDomain,
                Translations = l.Translations
            });
		}

		private void Search()
        {
            if (string.IsNullOrEmpty(Filter))
            {
				this.Lands = new ObservableCollection<PaisItemViewModel>(ToItemViewModel());
            }
            else
            {
				this.Lands = new ObservableCollection<PaisItemViewModel>(
					this.ToItemViewModel().Where(l => l.Name.ToLower().Contains(Filter.ToLower())
                                        || l.Capital.ToLower().Contains(Filter.ToLower())));
            }
        }
        #endregion
        #region Comandos
        public ICommand RefreshCommand
        {
            get { return new RelayCommand(LoadLands);  }
        }
        public ICommand SearchCommand
        {
            get { return new RelayCommand(Search); }
        }


        #endregion
    }
}
