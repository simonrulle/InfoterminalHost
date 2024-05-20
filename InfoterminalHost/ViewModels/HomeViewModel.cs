using InfoterminalHost.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using InfoterminalHost.Enums;
using InfoterminalHost.Models;

namespace InfoterminalHost.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        
        [ObservableProperty]
        private ObservableCollection<MediaItem> items = new ObservableCollection<MediaItem>();
        

        public HomeViewModel() 
        {
            var cd = new MediaItem
            {
                Id = 1,
                Name = "Classical Favorites",
                MediaType = ItemType.Music,
                MediumInfo = new Medium { Id = 1, Name = "Blu Ray", MediaType = ItemType.Book },
                Location = LocationType.InCollection
            };

            var book = new MediaItem
            {
                Id = 2,
                Name = "Classic Fairy Tales",
                MediaType = ItemType.Book,
                MediumInfo = new Medium { Id = 2, Name = "Hardcover", MediaType = ItemType.Book },
                Location = LocationType.InCollection
            };

            var bluRay = new MediaItem
            {
                Id = 3,
                Name = "The Mummy",
                MediaType = ItemType.Video,
                MediumInfo = new Medium { Id = 3, Name = "Blu Ray", MediaType = ItemType.Video },
                Location = LocationType.InCollection
            };

            
            items.Add(cd);
            items.Add(book);
            items.Add(bluRay);
            
        }


    }
}
