using InfoterminalHost.Enums;
using InfoterminalHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.Interfaces
{
    public interface ICafeteriaDataService
    {
        Task<MealPlan> GetMealPlan();
    }
}
