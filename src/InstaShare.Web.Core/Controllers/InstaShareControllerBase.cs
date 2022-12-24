using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace InstaShare.Controllers
{
    public abstract class InstaShareControllerBase: AbpController
    {
        protected InstaShareControllerBase()
        {
            LocalizationSourceName = InstaShareConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
