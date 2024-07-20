using CommunityToolkit.Mvvm.ComponentModel;
using InfoterminalHost.Interfaces;
using InfoterminalHost.Models;
using InfoterminalHost.Services;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.ViewModels
{
    public partial class CafeteriaViewModel : ObservableObject
    {
        private ICafeteriaDataService _cafeteriaDataService;

        private MealPlan mealPlan;

        [ObservableProperty]
        private ObservableCollection<Offering> offerings = new ObservableCollection<Offering>();

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private string selectedItem;

        [ObservableProperty]
        private ObservableCollection<Category> selectedCategories = new ObservableCollection<Category>();

        [ObservableProperty]
        private ObservableCollection<string> daten = new ObservableCollection<string>();

        public CafeteriaViewModel(ICafeteriaDataService cafeteriaDataService) 
        {
            _cafeteriaDataService = cafeteriaDataService;
            PopulateDataAsync();
        }

        private async void PopulateDataAsync()
        {
            try
            {
                mealPlan = await _cafeteriaDataService.GetMealPlan();

                foreach (Offering offering in mealPlan.Offering) 
                {
                    Offerings.Add(offering);
                    Daten.Add(offering.Date);
                }

                SelectedItem = Daten[0];

                IsLoading = false;
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Fehler beim Abrufen oder Verarbeiten der JSON-Daten: {ex.Message}");

                IsLoading = false;
            }
        }

        public void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.SelectedItem != null)
            {
                SelectedItem = comboBox.SelectedItem as string;
            }
            PopulateCategories(SelectedItem);
        }

        private void PopulateCategories(string date)
        {
            Offering offering = Offerings.Single(o => o.Date == date);

            ObservableCollection<Category> categories = new ObservableCollection<Category>();
            
            foreach (Category category in offering.Categories)
            {
                categories.Add(category);
            }

            SelectedCategories = categories;
        }

    }
}
