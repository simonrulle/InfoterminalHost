using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using InfoterminalHost.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using InfoterminalHost.Models;
using InfoterminalHost.Interfaces;
using InfoterminalHost.Services;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace InfoterminalHost.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TimetablesPage : Page
    {
        public TimetablesViewModel ViewModel;

        private readonly INavigationService _navigationService;

        public TimetablesPage()
        {
            ViewModel = App.HostContainer.Services.GetService<TimetablesViewModel>();
            this.InitializeComponent();
            _navigationService = NavigationService.Instance;
        }

        private void SemesterButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Semester semester)
            {
                _navigationService.Navigate(typeof(Views.TimetablesDetailsPage), semester.Timetable);
            }
        }
    }
}
