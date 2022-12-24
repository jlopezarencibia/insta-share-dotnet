using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using InstaShare.Configuration;

namespace InstaShare.Web.Host.Startup
{
    [DependsOn(
       typeof(InstaShareWebCoreModule))]
    public class InstaShareWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public InstaShareWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(InstaShareWebHostModule).GetAssembly());
        }
    }
}
