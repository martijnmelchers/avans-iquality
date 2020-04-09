using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models;
using IQuality.Models.Authentication;
using IQuality.Models.Helpers;
using Raven.Client.Documents.Session;

namespace IQuality.Infrastructure.Database.Repositories
{
    [Injectable(interfaceType: typeof(IInviteRepository))]
    public class InviteRepository : BaseRavenRepository<Invite>, IInviteRepository
    {
        public InviteRepository(IAsyncDocumentSession session) : base(session)
        {
        }

        public override async Task SaveAsync(Invite entity)
        {
            await Session.StoreAsync(entity);
        }

        public override void Delete(Invite entity)
        {
            Session.Delete(entity);
        }

        protected override async Task<List<Invite>> ConvertAsync(List<Invite> storage)
        {
            foreach (var registrationLink in storage)
                registrationLink.ApplicationUser =
                    await Session.LoadAsync<ApplicationUser>(registrationLink.ApplicationUserId);

            return storage;
        }
    }
}