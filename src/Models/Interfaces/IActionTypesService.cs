using IQuality.Models.Actions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.Models.Interfaces
{
    public interface IActionTypesService
    {
        List<string> GetActionTypes();
    }
}
