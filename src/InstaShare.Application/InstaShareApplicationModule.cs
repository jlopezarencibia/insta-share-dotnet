using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using InstaShare.Authorization;

namespace InstaShare
{
    [DependsOn(
        typeof(InstaShareCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class InstaShareApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<InstaShareAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(InstaShareApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
