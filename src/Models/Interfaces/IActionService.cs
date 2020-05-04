﻿
using System.Collections.Generic;
using System.Threading.Tasks;
using Action = IQuality.Models.Actions.Action;

namespace IQuality.Models.Interfaces
{
    public interface IActionService
    {
        Task<Action> CreateAction(string chatId, string goalId, string description);
        Task<List<Action>> GetActions(string chatId);
        Task<bool> DeleteAction(string actionId);
    }
}