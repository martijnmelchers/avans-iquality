using System;
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

        public async Task<List<Patient>> GetAllPatientsOfDoctorAsync(string doctorId)
        {
            return await Session.Query<Patient>().OfType<Patient>().Where(x => x.DoctorId == doctorId).ToListAsync();
        }

        public async Task<List<string>> AddTipIdToPatient(string tipId, string patientId)
        {
            var patient = await Session.Query<Patient>().OfType<Patient>().Where(p => p.Id == patientId).FirstAsync();

            if (!patient.TipIds.Contains(tipId))
            {
                patient.TipIds.Add(tipId);
            }

            await Session.StoreAsync(patient);

            return patient.TipIds;
        }

        public async Task<List<string>> DeleteTipIdFromPatient(string tipId, string patientId)
        {
            var patient = await Session.Query<Patient>().OfType<Patient>().Where(p => p.Id == patientId).FirstAsync();

            if (patient.TipIds.Contains(tipId))
            {
                patient.TipIds.Remove(tipId);
            }

            await Session.StoreAsync(patient);

            return patient.TipIds;
        }

        public async Task<List<string>> InitializeTipIdsList(string patientId)
        {
            var patient = await Session.Query<Patient>().OfType<Patient>().Where(p => p.Id == patientId).FirstAsync();

            if (patient.TipIds == null)
            {
                patient.TipIds = new List<string>();
            }

            await Session.StoreAsync(patient);

            return patient.TipIds;
        }

        public async Task<string> GetRandomTipIdFromPatient(string patientId)
        {
            var patient = await Session.Query<Patient>().OfType<Patient>().Where(p => p.ApplicationUserId == patientId).FirstAsync();

            string returnTipId = "-1";

            // if tipIds is initialized and tipIds has a tip
            if (patient.TipIds != null && patient.TipIds.Count != 0)
            {
                Random rnd = new Random();
                returnTipId = patient.TipIds[rnd.Next(patient.TipIds.Count)];
            }

            return returnTipId;
        }

        public async Task<List<string>> GetNotificationIdsOfPatient(string patientId)
        {
            var patient = await Session.Query<Patient>().OfType<Patient>().Where(p => p.ApplicationUserId == patientId).FirstAsync();

            return patient.NotificationIds;
        }

        public async Task<List<string>> SetPatientNotificationIdAsync(string notificationId, bool isSubscribed, string patientId)
        {
            var patient = await Session.Query<Patient>().OfType<Patient>().Where(p => p.ApplicationUserId == patientId).FirstAsync();

            if (patient.NotificationIds == null)
            {
                patient.NotificationIds = new List<string>();
            }

            if (isSubscribed)
                patient.NotificationIds.Add(notificationId);
            else
                patient.NotificationIds.Remove(notificationId);

            await Session.StoreAsync(patient);

            return patient.NotificationIds;
        }
    }
}