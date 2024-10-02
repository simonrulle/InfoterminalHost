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
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InfoterminalHost.ViewModels
{
    public partial class PersonsViewModel : ObservableObject
    {
        private IPersonsDataService _personsDataService;

        [ObservableProperty]
        private bool isDataLoadingError = false;

        public ObservableCollection<Person> persons { get; set; }

        public PersonsViewModel(IPersonsDataService personsDataService)
        {
            _personsDataService = personsDataService;
            PopulateData();
        }
 
        private void PopulateData()
        {
            try
            {
                persons = _personsDataService.GetPersonList();               
            }
            catch  
            {
                IsDataLoadingError = true;
            }
        }
    }

    
}
