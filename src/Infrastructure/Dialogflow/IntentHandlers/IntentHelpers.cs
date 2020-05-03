using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Interfaces;

namespace IQuality.Infrastructure.Dialogflow.IntentHandlers
{
    public static class IntentHelpers
    {
        public static List<Listable> ToListable<T>(this List<T> list) where T : IListable
        {
            return list.Select(item => item.ToListable()).ToList();
        } 
    }
}