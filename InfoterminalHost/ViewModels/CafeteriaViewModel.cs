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

        public CafeteriaViewModel(ICafeteriaDataService cafeteriaDataService) 
        { 
            _cafeteriaDataService = cafeteriaDataService;

            PopulateData();
        }

        public async void PopulateData()
        {
            await _cafeteriaDataService.PopulateData();
        }

    }
}
