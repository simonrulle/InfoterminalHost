using Azure;
using CommunityToolkit.Mvvm.ComponentModel;
using InfoterminalHost.Clients;
using InfoterminalHost.Interfaces;
using InfoterminalHost.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InfoterminalHost.ViewModels
{
    public partial class AssistantViewModel : ObservableObject
    {
        private ICafeteriaDataService _cafeteriaDataService;

        private PredictionHandler predictionHandler;

        private MealPlan mealPlan;

        [ObservableProperty]
        bool isLoading;

        [ObservableProperty]
        string prompt;

        public ObservableCollection<ExtendedDish> filteredDishes { get; set; }

        public ObservableCollection<Person> filteredPersons { get; set; }



        public AssistantViewModel(ICafeteriaDataService cafeteriaDataService)
        {
            _cafeteriaDataService = cafeteriaDataService;
            this.predictionHandler = new PredictionHandler();
            isLoading = false;
            prompt = "";
            mealPlan = new MealPlan();

            filteredDishes = new ObservableCollection<ExtendedDish>();
            filteredPersons = new ObservableCollection<Person>();
            
            MakeTestPredictionAsync();
        }

        public async void MakeTestPredictionAsync()
        {
            try
            {
                Response response = await predictionHandler.MakePredictionAsync("Welche Gerichte gibt es Mittwoch in der Mensa die günstiger sind als 2 Euro?");
                JsonResult result = JsonConvert.DeserializeObject<JsonResult>(response.Content.ToString());
            }
            catch (RequestFailedException ex)
            {
                throw new RequestFailedException(ex.ToString());
            }
        }

        public void OnButtonClick(object sender, RoutedEventArgs e)
        {
            IsLoading = true;

           

        }
    }
}
