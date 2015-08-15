using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    interface IConvertFloatToString
    {
        string AddAnd(string inputString);
        string GetCurrentInputValue(string inputString, int loopCounter);
    }
}
