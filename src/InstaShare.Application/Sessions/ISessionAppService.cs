using System.Threading.Tasks;
using Abp.Application.Services;
using InstaShare.Sessions.Dto;

namespace InstaShare.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
