using Azure;
using CommunityToolkit.Mvvm.ComponentModel;
using InfoterminalHost.Clients;
using InfoterminalHost.Enums;
using InfoterminalHost.Interfaces;
using InfoterminalHost.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InfoterminalHost.ViewModels
{
    public partial class AssistantViewModel : ObservableObject
    {
        private ICafeteriaDataService _cafeteriaDataService;

        private IRoomsDataService _roomsDataService;

        private IMapperService _mapperService;

        private PredictionHandler predictionHandler;

        [ObservableProperty]
        bool isLoading;

        [ObservableProperty]
        string prompt;

        [ObservableProperty]
        string loadingSpinnerVisibilityStatus;

        [ObservableProperty]
        string filteredDishesVisibilityStatus;

        public ObservableCollection<ExtendedDish> filteredDishes { get; set; }


        [ObservableProperty]
        string filteredPersonsVisibilityStatus;

        public ObservableCollection<Person> filteredPersons { get; set; }



        public AssistantViewModel(ICafeteriaDataService cafeteriaDataService, IRoomsDataService roomsDataService, IMapperService mapperService)
        {
            _cafeteriaDataService = cafeteriaDataService;
            _roomsDataService = roomsDataService;
            _mapperService = mapperService;
            this.predictionHandler = new PredictionHandler();
            isLoading = false;
            prompt = "";
            filteredDishesVisibilityStatus = VisibilityTypes.Collapsed.ToString();
            filteredPersonsVisibilityStatus = VisibilityTypes.Collapsed.ToString();
            loadingSpinnerVisibilityStatus = VisibilityTypes.Collapsed.ToString();
            filteredDishes = new ObservableCollection<ExtendedDish>();
            filteredPersons = new ObservableCollection<Person>();
            _roomsDataService = roomsDataService;
        }

        public async void OnAiSearchClick(object sender, RoutedEventArgs e)
        {
            FilteredDishesVisibilityStatus = VisibilityTypes.Collapsed.ToString();
            FilteredPersonsVisibilityStatus = VisibilityTypes.Collapsed.ToString();
            filteredDishes.Clear();
            filteredPersons.Clear();

            IsLoading = true;
            LoadingSpinnerVisibilityStatus = VisibilityTypes.Visible.ToString();

            JsonResult prediction = await MakePredictionAsync(Prompt);

            FilterObject filter = _mapperService.MapEntitiesToFilterObject(prediction.Result.Prediction.Entities);

            switch (prediction.Result.Prediction.TopIntent)
            {
                case "FilterMealPlan":
                    MealPlan mealPlan = await _cafeteriaDataService.GetMealPlan();
                    List<ExtendedDish> newDishList = _mapperService.MapMealPlanToExtendedDishes(mealPlan);
                    List<ExtendedDish> filteredList = ApplyDishesFilter(newDishList, filter);
                    foreach (ExtendedDish dish in filteredList)
                    {
                        filteredDishes.Add(dish);
                    }
                    FilteredDishesVisibilityStatus = VisibilityTypes.Visible.ToString();
                    break;

                case "SearchPerson":
                    ObservableCollection<Person> persons = _roomsDataService.GetPersonList();
                    var x = ApplyPersonsFilter(persons, filter);
                    foreach (Person person in x)
                    {
                        filteredPersons.Add(person);
                    }
                    FilteredPersonsVisibilityStatus = VisibilityTypes.Visible.ToString();
                    break;

                default:
                    break;
            }
            LoadingSpinnerVisibilityStatus = VisibilityTypes.Visible.ToString();
            IsLoading = false;          
        }

        private async Task<JsonResult> MakePredictionAsync(string prompt)
        {
            try
            {
                Response response = await predictionHandler.MakePredictionAsync(prompt);
                JsonResult result = JsonConvert.DeserializeObject<JsonResult>(response.Content.ToString());
                return result;
            }
            catch (RequestFailedException ex)
            {
                throw new RequestFailedException(ex.ToString());
            }
        }

        private List<ExtendedDish> ApplyDishesFilter(IEnumerable<ExtendedDish> dishList, FilterObject filter)
        {
            var query = dishList.AsQueryable();

            if (filter.DateInfo != null)
            {
                switch (filter.DateInfo.DateType)
                {
                    case "TemporalSpanResolution":
                        // Konvertierung in DateTime für Datumsoperationen
                        DateTime startDate = DateTime.Parse(filter.DateInfo.StartDate);
                        DateTime endDate = DateTime.Parse(filter.DateInfo.EndDate);
                        query = query.Where(o => o.Date != null && DateTime.Parse(o.Date) >= startDate && DateTime.Parse(o.Date) <= endDate);
                        break;

                    case "DateTimeResolution":
                        query = query.Where(o => o.Date != null && o.Date.Contains(filter.DateInfo.Date));
                        break;

                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(filter.CategoryName))
            {
                query = query.Where(o => o.CategoryName != null && o.CategoryName.Contains(filter.CategoryName));
            }

            if (!string.IsNullOrEmpty(filter.DishName))
            {
                query = query.Where(o => o.Name != null && o.Name.Contains(filter.DishName));
            }

            // Price TODO
            /*
            if (filter.Price.HasValue)
            {
                query = query.Where(o => o.Alter == filter.Alter.Value);
            }
            */

            if (!string.IsNullOrEmpty(filter.Ingredient))
            {
                query = query.Where(o => o.Ingredient != null && o.Ingredient.Contains(filter.Ingredient));
            }

            return query.ToList();
        }

        private List<Person> ApplyPersonsFilter(IEnumerable<Person> persons, FilterObject filter)
        {
            var query = persons.AsQueryable();

            if (!string.IsNullOrEmpty(filter.PersonName))
            {
                query = query.Where(o => o.Fullname != null && o.Fullname.Contains(filter.PersonName));
            }

            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(o => o.Title != null && o.Title.Contains(filter.Title));
            }

            if (!string.IsNullOrEmpty(filter.Role))
            {
                query = query.Where(o => o.Role != null && o.Role.Contains(filter.Role));
            }

            if (!string.IsNullOrEmpty(filter.Faculty))
            {
                query = query.Where(o => o.Faculty != null && o.Faculty.Contains(filter.Faculty));
            }

            if (!string.IsNullOrEmpty(filter.PhoneNumber))
            {
                query = query.Where(o => o.PhoneNumber != null && o.PhoneNumber.Contains(filter.PhoneNumber));
            }

            if (!string.IsNullOrEmpty(filter.Building))
            {
                query = query.Where(o => o.Building != null && o.Building.Contains(filter.Building));
            }

            if (!string.IsNullOrEmpty(filter.Room))
            {
                query = query.Where(o => o.Room != null && o.Room.Contains(filter.Room));
            }

            if (!string.IsNullOrEmpty(filter.Email))
            {
                query = query.Where(o => o.Emails != null && o.Emails.Contains(filter.Email));
            }

            return query.ToList();
        }
    }
}
