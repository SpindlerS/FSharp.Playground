using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Chess.App.Server.Services
{
    public interface IApplicationDataService
    {
        Task<object> GetApplicationData(HttpContext context);
    }
}