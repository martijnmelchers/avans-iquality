using System.Threading.Tasks;
using IQuality.Models.Authentication;

namespace IQuality.Infrastructure.Database.Repositories.Interface
{
    public interface IInviteRepository : IBaseRavenRepository<Invite>
    {
        Task<Invite> GetByInviteToken(string inviteToken);
    }
}