using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using InstaShare.EntityFrameworkCore;
using InstaShare.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace InstaShare.Web.Tests
{
    [DependsOn(
        typeof(InstaShareWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class InstaShareWebTestModule : AbpModule
    {
        public InstaShareWebTestModule(InstaShareEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(InstaShareWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(InstaShareWebMvcModule).Assembly);
        }
    }
}