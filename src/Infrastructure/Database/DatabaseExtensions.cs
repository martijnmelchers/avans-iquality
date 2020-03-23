using System.Collections.Generic;
using System.Threading.Tasks;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;

namespace IQuality.Infrastructure.Database
{
    public static class DatabaseExtensions
    {
        public static async Task<List<T>> Stream<T>(this IAsyncDocumentSession session, IRavenQueryable<T> query)
        {
            var result = new List<T>();

            using (var enumerator = await session.Advanced.StreamAsync(query))
            {
                while (await enumerator.MoveNextAsync())
                    if (enumerator.Current != null)
                        result.Add(enumerator.Current.Document);
            }

            return result;
        }
    }
}