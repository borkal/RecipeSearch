using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Utilities.Parsers
{
    interface IParser
    {
        void GetImage();
        void GetTitle();
        void GetDescription();
        string GetIngredients(string rec);

    }
}
