using IQuality.Models.Chat.Messages;

namespace IQuality.Models.Interfaces
{
    public interface IListable
    {
        public Listable ToListable(bool clickable = false, bool removable = false);
    }
}