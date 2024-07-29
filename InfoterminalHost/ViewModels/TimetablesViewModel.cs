using CommunityToolkit.Mvvm.ComponentModel;
using InfoterminalHost.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.ViewModels
{
    public partial class TimetablesViewModel : ObservableObject
    {
        private ITimetablesDataService _timetablesDataService;

        public TimetablesViewModel(ITimetablesDataService timetablesDataService) 
        {
            _timetablesDataService = timetablesDataService;
            var courses = _timetablesDataService.GetCoursesOfStudy();
        }
    }
}
