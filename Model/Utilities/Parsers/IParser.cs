using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Utilities.Parsers
{
    public interface IParser
    {
        string GetImage();
        void GetTitle();
        List<string> GetDescription();
        List<string> GetIngredients();

    }
}
