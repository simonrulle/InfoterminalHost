using InfoterminalHost.Interfaces;
using InfoterminalHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InfoterminalHost.Services
{
    public class MapperService : IMapperService
    {
        public MapperService() { }

        public FilterObject MapEntitiesToFilterObject(List<Entity> entities) 
        {
            // Filterobjekt, welches zurückgegeben wird
            FilterObject filterObject = new FilterObject();
            Entity entity;

            // Map Building
            entity = entities.FirstOrDefault(x => x.Category == "Building") ?? null;
            if (entity != null)
            {
                filterObject.Building = Regex.Replace(entity.Text, @"[^\d]", "") ?? entity.Text;
            }
            else 
            { 
                filterObject.Building = null;
            }
            entity = null;

            // Map CategoryName
            entity = entities.FirstOrDefault(x => x.Category == "CategoryName") ?? null;
            if (entity != null)
            {
                filterObject.CategoryName = entity.Text;
            }
            else
            {
                filterObject.CategoryName = null;
            }
            entity = null;

            /*
             * 
             * TODO DATE FILTER
             * 
             */
            filterObject.Date = null;


            // Map DishName
            entity = entities.FirstOrDefault(x => x.Category == "DishName") ?? null;
            if (entity != null)
            {
                filterObject.DishName = entity.Text;
            }
            else
            {
                filterObject.DishName = null;
            }
            entity = null;

            // Map Email
            entity = entities.FirstOrDefault(x => x.Category == "Email") ?? null;
            if (entity != null)
            {
                filterObject.Email = entity.Text;
            }
            else
            {
                filterObject.Email = null;
            }
            entity = null;

            /*
             * 
             * TODO FACULTY FILTER
             * 
             */
            filterObject.Faculty = null;


            /*
             * 
             * TODO INGREDIENT FILTER
             * 
             */
            filterObject.Ingredient = null;


            // Map PersonName
            entity = entities.FirstOrDefault(x => x.Category == "PersonName") ?? null;
            if (entity != null)
            {
                filterObject.PersonName = entity.Text;
            }
            else
            {
                filterObject.PersonName = null;
            }
            entity = null;

            // Map PhoneNumber
            entity = entities.FirstOrDefault(x => x.Category == "PhoneNumber") ?? null;
            if (entity != null)
            {
                filterObject.PhoneNumber = entity.Text;
            }
            else
            {
                filterObject.PhoneNumber = null;
            }
            entity = null;

            // Map Price
            entity = entities.FirstOrDefault(x => x.Category == "Price") ?? null;
            if (entity != null)
            {
                string pattern = @"\b\d{1,3}(?:\.\d{3})*(?:,\d{2})?\b";
                Match match = Regex.Match(entity.Text, pattern);
                if (match.Success)
                {
                    filterObject.Price = match.Value;
                }
                else
                {
                    filterObject.Price= null;
                }
            }
            else
            {
                filterObject.Price = null;
            }
            entity = null;

            // Map PriceCategory
            entity = entities.FirstOrDefault(x => x.Category == "PriceCategory") ?? null;
            if (entity != null)
            {
                filterObject.PriceCategory = entity.ExtraInformations[0]?.Key ?? entity.Text;
            }
            else
            {
                filterObject.PriceCategory = null;
            }
            entity = null;

            // Map PriceInterpretation
            entity = entities.FirstOrDefault(x => x.Category == "PriceInterpretation") ?? null;
            if (entity != null)
            {
                filterObject.PriceInterpretation = entity.ExtraInformations[0]?.Key ?? entity.Text;
            }
            else
            {
                filterObject.PriceInterpretation = null;
            }
            entity = null;

            // Map Role
            entity = entities.FirstOrDefault(x => x.Category == "Role") ?? null;
            if (entity != null)
            {
                filterObject.Role = entity.Text;
            }
            else
            {
                filterObject.Role = null;
            }
            entity = null;

            // Map Room
            entity = entities.FirstOrDefault(x => x.Category == "Room") ?? null;
            if (entity != null)
            {
                filterObject.Room = Regex.Replace(entity.Text, @"[^\d]", "") ?? entity.Text;
            }
            else
            {
                filterObject.Room = null;
            }
            entity = null;

            // Map Title
            entity = entities.FirstOrDefault(x => x.Category == "Title") ?? null;
            if (entity != null)
            {
                filterObject.Title = entity.ExtraInformations[0]?.Key ?? entity.Text;
            }
            else
            {
                filterObject.Title = null;
            }
            entity = null;

            // return vorbereitetes Filter-Objekt
            return filterObject;
        }

        public List<ExtendedDish> MapMealPlanToExtendedDishes(MealPlan mealPlan)
        {
            List<ExtendedDish> listToReturn = new List<ExtendedDish>();

            foreach (Offering offering in mealPlan.Offering)
            {
                foreach (Category category in offering.Categories)
                {
                    foreach (Dish dish in category.Dishes)
                    {
                        ExtendedDish newDish = new ExtendedDish();
                        newDish.Date = offering.Date;
                        newDish.CategoryName = category.Name;
                        newDish.Name = dish.Name;
                        newDish.StudentPrice = dish.StudentPrice;
                        newDish.EmployeePrice = dish.EmployeePrice;
                        newDish.GuestPrice = dish.GuestPrice;
                        newDish.Ingredient = dish.Ingredient;                            
                        listToReturn.Add(newDish);
                    }

                }
                
            }

            return listToReturn;
        }


    }
}
