using CommunityToolkit.Mvvm.ComponentModel;
using HtmlAgilityPack;
using InfoterminalHost.Interfaces;
using InfoterminalHost.Models;
using InfoterminalHost.Services;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InfoterminalHost.ViewModels
{
    public partial class RoomsViewModel : ObservableObject
    {
        private IRoomsDataService _roomsDataService;

        List<Person> personList;

        [ObservableProperty]
        List<Person> personsViewList;

        [ObservableProperty]
        private bool isDataLoadingError = false;

        public RoomsViewModel(IRoomsDataService roomsDataService)
        {
            _roomsDataService = roomsDataService;
            PopulateData();
        }
 
        private void PopulateData()
        {
            try
            {
                personList = PersonsViewList = _roomsDataService.GetPersonList();               
            }
            catch  
            {
                IsDataLoadingError = true;
            }
        }
    }
}
