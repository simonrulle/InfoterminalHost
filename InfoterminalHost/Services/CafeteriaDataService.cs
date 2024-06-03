using InfoterminalHost.Enums;
using InfoterminalHost.Interfaces;
using InfoterminalHost.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.System;

namespace InfoterminalHost.Services
{
    public class CafeteriaDataService : ICafeteriaDataService
    {
        private HttpClient client;

        public CafeteriaDataService()
        {
            client = new HttpClient();
        }

        public async Task<MealPlan> GetMealPlan()
        {
            // URL der JSON-Datenquelle
            string url = "https://speiseplan.stw-greifswald.de/speiseplan_json_hst.php";
            
            // JSON-Daten von der URL abrufen
            string jsonString = await client.GetStringAsync(url);

            // JSON-Daten deserialisieren
            MealPlan mealPlan = JsonConvert.DeserializeObject<MealPlan>(jsonString);
            
            // Speiseplan zurückgeben
            return mealPlan;
        }
    }
}
