using InfoterminalHost.Interfaces;
using InfoterminalHost.Services;
using InfoterminalHost.ViewModels;
using InfoterminalHost.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace InfoterminalHost
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        public static IHost HostContainer { get; private set; }
        
        private Window m_window;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            RegisterComponents();
            m_window = new MainWindow();
            m_window.Activate();
        }

        /// <summary>
        /// Initializes DI-Container
        /// </summary>
        private void RegisterComponents()
        {
            HostContainer = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<ICafeteriaDataService, CafeteriaDataService>();
                services.AddSingleton<IRoomsDataService, RoomsDataService>();
                services.AddSingleton<IMapperService, MapperService>();
                services.AddTransient<HomeViewModel>();
                services.AddTransient<AssistantViewModel>();
                services.AddTransient<CafeteriaViewModel>();
                services.AddTransient<RoomsViewModel>(); 
                services.AddTransient<TimetablesViewModel>();
            }).Build();
        }

    }
}
