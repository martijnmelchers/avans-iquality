using IQuality.Models.Actions;
using IQuality.Models.Helpers;
using IQuality.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.DomainServices.Services
{
    [Injectable]
    public class ActionTypesService : IActionTypesService
    {
        public List<string> GetActionTypes()
        {
            var result = ActionTypes.GetActionTypes;

            List<string> actionTypesStrings = new List<string>();

            foreach (ActionTypes actionTypes in result)
            {
                actionTypesStrings.Add(actionTypes.GetActionType());
            }

            return actionTypesStrings;
        }
    }
}
