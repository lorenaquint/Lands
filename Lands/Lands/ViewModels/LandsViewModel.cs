
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
        private bool isRefreshing;
        private string filter;
        private List<Land> landList;
        #endregion
        #region Propiedades
        private ObservableCollection<Land> lands;


        #endregion

        #region Propiedades
        public ObservableCollection<Land> Lands
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
            landList = (List<Land>)response.Result;
            this.Lands = new ObservableCollection<Land>(landList);

        }
        private void Search()
        {
            if (string.IsNullOrEmpty(Filter))
            {
                this.Lands = new ObservableCollection<Land>(this.landList);
            }
            else
            {
                this.Lands = new ObservableCollection<Land>(
                    this.landList.Where(l => l.Name.ToLower().Contains(Filter.ToLower())
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
