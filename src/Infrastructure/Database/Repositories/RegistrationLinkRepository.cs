using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models;
using IQuality.Models.Authentication;
using Raven.Client.Documents.Session;

namespace IQuality.Infrastructure.Database.Repositories
{
    public class RegistrationLinkRepository : BaseRavenRepository<RegistrationLink>
    {
        public RegistrationLinkRepository(IAsyncDocumentSession session) : base(session)
        {
        }

        public override async Task SaveAsync(RegistrationLink entity)
        {
            await Session.StoreAsync(entity);
        }

        public override void DeleteAsync(RegistrationLink entity)
        {
            Session.Delete(entity);
            return;
        }

        protected override async Task<List<RegistrationLink>> ConvertAsync(List<RegistrationLink> storage)
        {
            foreach (var registrationLink in storage)
            {
               registrationLink.ApplicationUser = await Session.LoadAsync<ApplicationUser>(registrationLink.ApplicationUserId);
            }
            return storage;
        }
    }
}
