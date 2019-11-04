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
        public List<Ingredient> SelectIngredients(OdbcDataReader dataReader)
        {
            var ingredientList = new List<Ingredient>();
            while (dataReader.Read())
            {
                Ingredient ingredientToList = new Ingredient();
                ingredientToList.IngredientId = dataReader.GetInt32(0);
                ingredientToList.IngredientCategoryIds = dataReader[1] == DBNull.Value ? new List<int>() : ConvertStringToIntList(dataReader.GetValue(1).ToString());
                ingredientToList.IngredientName = dataReader[2] == DBNull.Value ? "" : dataReader.GetString(2);
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

        public List<IngredientCategory> SelectIngredientCategoriesMapper(OdbcDataReader dataReader)
        {
            var ingredientCategoryList = new List<IngredientCategory>();
            while (dataReader.Read())
            {
                var ingredientCategory = new IngredientCategory();
                ingredientCategory.IngredientCategoryId = dataReader.GetInt32(0);
                ingredientCategory.IngredientCategoryName = dataReader[1] == DBNull.Value ? "" : dataReader.GetString(1);

                ingredientCategoryList.Add(ingredientCategory);
            }

            return ingredientCategoryList;
        }

        public List<IngredientCategoryXref> SelectIngriedentCategoiresXrefMapper(OdbcDataReader dataReader)
        {
            var ingredientCategoryXrefList = new List<IngredientCategoryXref>();
            while (dataReader.Read())
            {
                var ingredientCategoryXref = new IngredientCategoryXref();
                ingredientCategoryXref.IngredientId = dataReader.GetInt32(0);
                ingredientCategoryXref.IngredientCategoryIds = dataReader[1] == DBNull.Value ? 0 : Convert.ToInt32(dataReader.GetValue(1));

                ingredientCategoryXrefList.Add(ingredientCategoryXref);
            }

            return ingredientCategoryXrefList;
        }

        private List<int> ConvertStringToIntList(string text)
        {
            if (!(string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text) || text == "NULL"))
            {
                return text.Replace(",NULL", "").Trim().Split(',').Select(x => Convert.ToInt32(x)).ToList();
            }

            return new List<int>();

        }


    }
}
