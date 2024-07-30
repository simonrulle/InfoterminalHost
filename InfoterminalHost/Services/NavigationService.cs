using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using InfoterminalHost.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.Services
{
    public class NavigationService : INavigationService
    {
        private static NavigationService _instance;
        private Frame _contentFrame;
        private NavigationView _navigationView;

        private NavigationService() { }

        public static NavigationService Instance => _instance ??= new NavigationService();

        public void Initialize(Frame contentFrame, NavigationView navigationView)
        {
            _contentFrame = contentFrame;
            _navigationView = navigationView;

            _navigationView.ItemInvoked += NavigationView_ItemInvoked;
            _contentFrame.Navigated += ContentFrame_Navigated;
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                _contentFrame.Navigate(typeof(Views.SettingsPage), null, args.RecommendedNavigationTransitionInfo);
            }
            else if (args.InvokedItemContainer != null && args.InvokedItemContainer.Tag != null)
            {
                Type newPage = Type.GetType(args.InvokedItemContainer.Tag.ToString());
                _contentFrame.Navigate(newPage, null, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            _navigationView.IsBackEnabled = _contentFrame.CanGoBack;

            if (_contentFrame.SourcePageType == typeof(Views.SettingsPage))
            {
                _navigationView.SelectedItem = (NavigationViewItem)_navigationView.SettingsItem;
            }
            else if (_contentFrame.SourcePageType == typeof(Views.TimetablesDetailsPage))
            {
                _navigationView.SelectedItem = null;
            }
            else if (_contentFrame.SourcePageType != null)
            {
                _navigationView.SelectedItem = _navigationView.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals(_contentFrame.SourcePageType.FullName));
            }
        }

        public void GoBack()
        {
            if (_contentFrame.CanGoBack)
            {
                _contentFrame.GoBack();
            }
        }

        public void Navigate(Type pageType, object parameter = null)
        {
            _contentFrame.Navigate(pageType, parameter);
        }
    }
}
