using InfoterminalHost.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using InfoterminalHost.Models;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using InfoterminalHost.Interfaces;
using InfoterminalHost.Services;
using System.Diagnostics.Metrics;

namespace InfoterminalHost.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        public HomeViewModel() 
        {
            _navigationService = NavigationService.Instance;
        }

        public void OnButtonClick(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                string buttonContent = clickedButton.Content.ToString();

                switch (buttonContent)
                {
                    case "KI-Assistent":
                        _navigationService.Navigate(typeof(Views.AssistantPage));
                        break;

                    case "Mensaplan":
                        _navigationService.Navigate(typeof(Views.CafeteriaPage));
                        break;

                    case "Personenplan":
                        _navigationService.Navigate(typeof(Views.PersonsPage));
                        break;

                    case "Neuigkeiten":
                        break;

                    case "Busplan":
                        break;

                    case "Stundenpläne":
                        _navigationService.Navigate(typeof(Views.TimetablesPage));
                        break;

                    case "Wetter":
                        break;

                    case "Einstellungen":
                        _navigationService.Navigate(typeof(Views.SettingsPage));
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
