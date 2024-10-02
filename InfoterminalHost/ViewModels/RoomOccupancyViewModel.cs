using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.ViewModels
{
    public partial class RoomOccupancyViewModel : ObservableObject
    {
        [ObservableProperty]
        private string roomOccupancyTableSource;

        public RoomOccupancyViewModel()
        {
            roomOccupancyTableSource = "https://infoserver.hochschule-stralsund.de/davinci-now.html?&refreshInterval=600000&flipInterval=10000&substCols=date|weekDay|pos|time|teacher|subject|room|class|info|message&filter.bu=H21&nowScope=day&nowView=time";
        }
    }
}
