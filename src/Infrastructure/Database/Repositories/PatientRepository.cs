using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Authentication;
using IQuality.Models.Helpers;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace IQuality.Infrastructure.Database.Repositories
{
    [Injectable(interfaceType: typeof(IPatientRepository))]
    public class PatientRepository : BaseRavenRepository<Patient>, IPatientRepository
    {
        public PatientRepository(IAsyncDocumentSession session) : base(session)
        {
            
        }

        public override void Delete(Patient entity)
        {
            Session.Delete(entity);
        }

        protected override Task<List<Patient>> ConvertAsync(List<Patient> storage)
        {
            return Task.FromResult(storage.ToList());
        }

        public async Task<List<PatientApplicationUser>> GetAllPatientsOfDoctorAsync(string doctorId)
        {
            var patientList = await Session.Query<Patient>().OfType<Patient>().Where(x => x.DoctorId == doctorId).ToListAsync();

            var patientApplicationUserList = new List<PatientApplicationUser>();

            foreach (Patient patient in patientList)
            {
                var applicationUser = await Session.LoadAsync<ApplicationUser>(patient.ApplicationUserId);

                var patientApplicationUser = new PatientApplicationUser();

                patientApplicationUser.Id = patient.Id;
                patientApplicationUser.ApplicationUser = applicationUser;
                patientApplicationUser.DoctorId = patient.DoctorId;

                patientApplicationUserList.Add(patientApplicationUser);
            }

            return patientApplicationUserList;
        }
    }
}