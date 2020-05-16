using System.Threading.Tasks;
using IQuality.Models.PatientData;

namespace IQuality.Infrastructure.Database.Repositories.Interface
{
    public interface IPatientDataRepository:  IBaseRavenRepository<PatientData>
    {
    }
}