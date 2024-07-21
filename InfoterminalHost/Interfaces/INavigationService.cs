using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.Interfaces
{
    public interface INavigationService
    {
        void Initialize(Frame contentFrame, NavigationView navigationView);
        void GoBack();
        void Navigate(Type pageType, object parameter = null);
    }
}
