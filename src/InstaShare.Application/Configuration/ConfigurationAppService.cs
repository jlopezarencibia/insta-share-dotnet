using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using InstaShare.Configuration.Dto;

namespace InstaShare.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : InstaShareAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
