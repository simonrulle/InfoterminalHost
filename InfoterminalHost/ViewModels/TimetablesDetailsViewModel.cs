using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.ViewModels
{
    public partial class TimetablesDetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        private string timetableSource;

        public TimetablesDetailsViewModel() 
        {
            timetableSource = "https://www.hochschule-stralsund.de/storages/hs-stralsund/FAK_WS/Allgemein/FaK_WS_Stundenplaene/Sommersemester/Sommersemester_WInf-M.pdf";
        }
    }
}
