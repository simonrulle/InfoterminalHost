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
            filteredDishes = new ObservableCollection<ExtendedDish>();
            filteredPersons = new ObservableCollection<Person>();
            _roomsDataService = roomsDataService;
        }

        public async void OnAiSearchClick(object sender, RoutedEventArgs e)
        {
            FilteredDishesVisibilityStatus = VisibilityTypes.Collapsed.ToString();
            FilteredDishesVisibilityStatus = VisibilityTypes.Collapsed.ToString();
            filteredDishes.Clear();
            filteredPersons.Clear();

            IsLoading = true;

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
                    IsLoading = false;
                    break;

                case "SearchPerson":
                    ObservableCollection<Person> persons = _roomsDataService.GetPersonList();
                    var x = ApplyPersonsFilter(persons, filter);
                    foreach (Person person in x)
                    {
                        filteredPersons.Add(person);
                    }
                    FilteredPersonsVisibilityStatus = VisibilityTypes.Visible.ToString();
                    IsLoading = false;
                    break;

                default:
                    break;
            }
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

        private List<ExtendedDish> ApplyDishesFilter(List<ExtendedDish> dishList, FilterObject filter)
        {
            List<ExtendedDish> tempDishList = dishList;
            List<ExtendedDish> dishesToDelete = new List<ExtendedDish>();

            foreach (var d in dishList)
            {
                bool remove = false;

                // Filter Date
                if (filter.Date != null)
                {
                    if (!d.Date.Contains(filter.Date))
                    {
                        remove = true;
                    }
                }

                // Filter CategoryName
                if (filter.CategoryName != null)
                {
                    if (!d.CategoryName.Contains(filter.CategoryName))
                    {
                        remove = true;
                    }
                }

                // Filter DishName
                if (filter.DishName != null)
                {
                    if (!d.Name.Contains(filter.DishName))
                    {
                        remove = true;
                    }
                }

                // Filter CategoryName
                if (filter.CategoryName != null)
                {
                    if (!d.CategoryName.Contains(filter.CategoryName))
                    {
                        remove = true;
                    }
                }


                /*
                 * 
                 * TODO weitere Filter
                 * 
                 */

                if (remove)
                {
                    dishesToDelete.Add(d);
                }

            }

            foreach (var d in dishesToDelete)
            {
                tempDishList.Remove(d);
            }

            return tempDishList;
        }

        private ObservableCollection<Person> ApplyPersonsFilter(ObservableCollection<Person> persons, FilterObject filter)
        {
            ObservableCollection<Person> tempPersonList = persons;
            ObservableCollection<Person> PersonsToDelete = new ObservableCollection<Person>();

            foreach (var p in persons)
            {
                bool remove = false;

                // Filter Fullname
                if (filter.PersonName != null)
                {
                    if (!p.Fullname.Contains(filter.PersonName))
                    {
                        remove = true;
                    }
                }

                // Filter Titel
                if (filter.Title != null)
                {
                    if (!p.Title.Contains(filter.Title))
                    {
                        remove = true;
                    }
                }

                // Filter Role
                if (filter.Role != null)
                {
                    if (!p.Role.Contains(filter.Role))
                    {
                        remove = true;
                    }
                }

                // Filter faculty
                if (filter.Faculty != null)
                {
                    if (!p.Faculty.Contains(filter.Faculty))
                    {
                        remove = true;
                    }
                }

                // Filter PhoneNumber
                if (filter.PhoneNumber != null)
                {
                    if (!p.PhoneNumber.Contains(filter.PhoneNumber))
                    {
                        remove = true;
                    }
                }

                // Filter Building
                if (filter.Building != null)
                {
                    if (!p.Building.Contains(filter.Building))
                    {
                        remove = true;
                    }
                }

                // Filter Room
                if (filter.Room != null)
                {
                    if (!p.Room.Contains(filter.Room))
                    {
                        remove = true;
                    }
                }


                if (remove)
                {
                    PersonsToDelete.Add(p);
                }
            }

            foreach (var p in PersonsToDelete)
            {
                tempPersonList.Remove(p);
            }

            return tempPersonList;
        }
    }
}
