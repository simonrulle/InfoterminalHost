using InfoterminalHost.Interfaces;
using InfoterminalHost.Models;
using InfoterminalHost.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.ViewModels
{
    public partial class CafeteriaViewModel
    {
        private ICafeteriaDataService _cafeteriaDataService;

        private MealPlan mealPlan;

        public CafeteriaViewModel(ICafeteriaDataService cafeteriaDataService) 
        { 
            _cafeteriaDataService = cafeteriaDataService;
            PopulateData();
        }

        private async void PopulateData()
        {
            try
            {
                mealPlan = await _cafeteriaDataService.GetMealPlan();
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Fehler beim Abrufen oder Verarbeiten der JSON-Daten: {ex.Message}");
            }
        }

    }
}
