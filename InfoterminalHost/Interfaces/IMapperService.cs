using InfoterminalHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoterminalHost.Interfaces
{
    public interface IMapperService
    {
        public FilterObject MapEntitiesToFilterObject(List<Entity> entities);

        public List<ExtendedDish> MapMealPlanToExtendedDishes(MealPlan mealPlan);
    }
}
