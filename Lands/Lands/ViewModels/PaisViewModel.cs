
namespace Lands.ViewModels
{
	using System;
	using System.Collections.ObjectModel;
	using System.Linq;
	using Models;
	public class PaisViewModel:BaseViewModel
	{
		private ObservableCollection<Border> borders;
		private ObservableCollection<Currency> currencies;
		private ObservableCollection<Language> languages;
		#region Propiedades

		public ObservableCollection<Language> Languages
        {
			get
			{
				return languages;	
			}
	        set
			{
				SetValue(ref languages, value);
			}
        }
		public ObservableCollection<Border> Borders
        {
            get
            {
                return borders;
            }
            set
            {
                SetValue(ref borders, value);
            }
        }

		public ObservableCollection<Currency> Currencies
        {
            get
            {
				return currencies;
            }
            set
            {
				SetValue(ref currencies, value);
            }
        }
        public Land Land
		{
			get;
			set;
		}
		#endregion
        

        
		public PaisViewModel(Land land)
		{
			this.Land = land;
			this.LoadBorders();
			this.Currencies = new ObservableCollection<Currency>(this.Land.Currencies);
			this.Languages = new ObservableCollection<Language>(this.Land.Languages);
		}

		#region Metodos
		private void LoadBorders()
		{
			this.Borders = new ObservableCollection<Border>();
			foreach (var item in Land.Borders)
			{
				var land = MainViewModel.GetInstance()
										.LandsList
										.Where(l => l.Alpha3Code == item)
										.FirstOrDefault();
				if (land != null)
                {
					this.Borders.Add(new Border
					{
						Code = land.Alpha3Code,
						Name = land.Name,


					});

                }

			}
            
		}
		#endregion
	}
}
