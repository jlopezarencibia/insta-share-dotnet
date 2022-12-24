using Abp.Application.Services;
using InstaShare.MultiTenancy.Dto;

namespace InstaShare.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

