using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Domain
{
    public class RecipeFeatureXref
    {
        public int RecipeId { get; set; }
        public int FeatureId { get; set; }
    }
}


//zapytanie ktore dla recipe_id w jednej linii da liste feature_id's:
/*SELECT RF.recipe_id, Replace(Replace((CAST(array_agg(distinct RF.feature_id) AS VARCHAR)),'}',''), '{', '') as featuresIds FROM recipe_feature RF
GROUP BY RF.recipe_id*/

