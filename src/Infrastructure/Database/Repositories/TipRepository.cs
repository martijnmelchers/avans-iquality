using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Doctor;
using IQuality.Models.Helpers;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQuality.Infrastructure.Database.Repositories
{
    [Injectable(interfaceType: typeof(ITipRepository))]
    public class TipRepository : BaseRavenRepository<Tip>, ITipRepository
    {


        public TipRepository(IAsyncDocumentSession session) : base(session)
        {

        }

        public async Task<Tip> CreateTipAsync(Tip tip)
        {
            await Session.StoreAsync(tip);

            return tip;
        }

        public async Task<Tip> EditTipAsync(Tip edittedTip)
        {
            await Session.StoreAsync(edittedTip);

            return edittedTip;
        }

        public async Task<Tip> DeleteTipAsync(string tipId)
        {
            Tip tip = await Session.LoadAsync<Tip>(tipId);

            Session.Delete(tip);

            return tip;
        }

        public async Task<List<Tip>> GetTipsOfDoctorAsync(string doctorId)
        {
            return await Session.Query<Tip>().OfType<Tip>().Where(x => x.DoctorId == doctorId).ToListAsync();
        }

        protected override Task<List<Tip>> ConvertAsync(List<Tip> storage)
        {
            throw new NotImplementedException();
        }
    }
}
