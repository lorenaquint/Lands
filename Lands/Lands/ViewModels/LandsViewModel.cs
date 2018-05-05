
namespace Lands.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Models;
    using Services;
    using Xamarin.Forms;

    public class LandsViewModel:BaseViewModel
    {
        #region Services
        private ApiService apiService;
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
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error",
                                                                connection.Message,
                                                                "Aceptar");
                await Application.Current.MainPage.Navigation.PopAsync();
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
                return;
            }
            var miLista = (List<Land>)response.Result;
            this.Lands = new ObservableCollection<Land>(miLista);

        }
        #endregion
    }
}
