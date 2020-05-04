using System.Threading.Tasks;

namespace IQuality.Models.Interfaces
{
    public interface IActionService
    {
        Task CreateAction(string chatId, string description);
    }
}