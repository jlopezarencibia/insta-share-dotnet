using System.Threading.Tasks;
using Abp.Application.Services;
using InstaShare.Authorization.Accounts.Dto;

namespace InstaShare.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
