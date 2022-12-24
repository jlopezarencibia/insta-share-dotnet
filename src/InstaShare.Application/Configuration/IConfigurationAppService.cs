using System.Threading.Tasks;
using InstaShare.Configuration.Dto;

namespace InstaShare.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
