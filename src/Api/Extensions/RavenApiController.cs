using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Raven.Client.Documents.Session;

namespace IQuality.Api.Extensions
{
    public class RavenApiController : Controller
    {
        private readonly IAsyncDocumentSession _session;

        protected RavenApiController(IAsyncDocumentSession session)
        {
            _session = session;
        }

        // Save changes to database after request
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var executedContext = await next.Invoke();
            if (executedContext.Exception == null)
                await _session.SaveChangesAsync();
            
            _session.Dispose();
        }
    }
}