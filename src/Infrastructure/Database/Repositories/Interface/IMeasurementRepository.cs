using System.Threading.Tasks;
using IQuality.Models.Measurements;

namespace IQuality.Infrastructure.Database.Repositories.Interface
{
    public interface IMeasurementRepository:  IBaseRavenRepository<Measurement>
    {
    }
}