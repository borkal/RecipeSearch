using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Domain;

namespace Model.Utilities.ModelMapper
{
    public class IngredientMapper
    {
        public List<Ingredient> SelectAllIngredientsByRecipeIdMapper(OdbcDataReader dataReader)
        {
            var ingredientList = new List<Ingredient>();
            while (dataReader.Read())
            {
                Ingredient ingredientToList = new Ingredient();
                ingredientToList.IngredientId = dataReader.GetInt32(0);
                ingredientToList.IngredientName = dataReader[1] == DBNull.Value ? "" : dataReader.GetString(1);
                ingredientToList.IngredientParentId = dataReader.GetInt32(2);
                ingredientToList.IngredientCitrus = dataReader.GetBoolean(3);
                ingredientToList.IngredientNut = dataReader.GetBoolean(4);
                ingredientToList.IngredientSugar = dataReader.GetBoolean(5);
                ingredientToList.IngredientMushroom = dataReader.GetBoolean(6);
                ingredientToList.IngredientGluten = dataReader.GetBoolean(7);
                ingredientToList.IngredientCowMilk = dataReader.GetBoolean(8);
                ingredientToList.IngredientWheat = dataReader.GetBoolean(9);
                ingredientToList.IngredientEgg = dataReader.GetBoolean(10);
                ingredientToList.IngredientVegetarian = dataReader.GetBoolean(11);

                ingredientList.Add(ingredientToList);
            }
            

            return ingredientList;
        }
    }
}


/*
Jakie metody dla feature, dish? zastanowic sie co bedzie potrzebne
    - selectrecipefeaturebyrecipeid ???
    - jakie metody na front??? co bedzie potrzebne?
    - dokoncz dao ingredient, stworz dla feature i dish (testowo)
    - parsery
 */