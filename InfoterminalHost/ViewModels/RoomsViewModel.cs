using CommunityToolkit.Mvvm.ComponentModel;
using HtmlAgilityPack;
using InfoterminalHost.Interfaces;
using InfoterminalHost.Models;
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

        public RoomsViewModel(IRoomsDataService roomsDataService)
        {
            _roomsDataService = roomsDataService;
            personList = roomsDataService.GetPersonList();
        }
 
    }
}
