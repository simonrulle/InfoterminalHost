using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InfoterminalHost.Interfaces;
using InfoterminalHost.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InfoterminalHost.ViewModels
{
    public partial class TimetablesViewModel : ObservableObject
    {
        private ITimetablesDataService _timetablesDataService;

        public ObservableCollection<CourseOfStudy> courses { get; set; }

        public TimetablesViewModel(ITimetablesDataService timetablesDataService)
        {
            _timetablesDataService = timetablesDataService;
            courses = _timetablesDataService.GetCoursesOfStudy();
        }
    }
}
