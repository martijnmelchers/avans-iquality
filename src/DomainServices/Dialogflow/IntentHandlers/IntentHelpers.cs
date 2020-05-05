using System.Collections.Generic;
using System.Linq;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Interfaces;

namespace IQuality.DomainServices.Dialogflow.IntentHandlers
{
    public static class IntentHelpers
    {
        public static List<Listable> ToListable<T>(this List<T> list, bool clickable = false, bool removable = false) where T : IListable
        {
            return list.Select(item => item.ToListable(clickable, removable)).ToList();
        } 
    }
}