using System.Collections.Generic;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Interfaces;

namespace IQuality.Models.Actions
{
    public class ActionTypes : IListable
    {
        public static List<ActionTypes> GetActionTypes = new List<ActionTypes>()
        {
            new ActionTypes(ActionType.Weight),
            new ActionTypes(ActionType.BloodSugar),
            new ActionTypes(ActionType.BloodPressure),
            new ActionTypes(ActionType.Cholesterol),
            new ActionTypes(ActionType.General)
        };

        private readonly string _actionType;

        public ActionTypes(ActionType actionType)
        {
            _actionType = actionType.ToString();
        }

        public string GetActionType()
        {
            return _actionType;
        }

        public Listable ToListable(bool clickable = false, bool removable = false)
        {
            return new Listable(_actionType, null, clickable, removable);
        }
    }
    public enum ActionType
    {
        Weight,
        BloodSugar,
        BloodPressure,
        Cholesterol,
        General
    }

}