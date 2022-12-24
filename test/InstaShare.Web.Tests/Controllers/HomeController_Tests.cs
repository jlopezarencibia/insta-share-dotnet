using System.Threading.Tasks;
using InstaShare.Models.TokenAuth;
using InstaShare.Web.Controllers;
using Shouldly;
using Xunit;

namespace InstaShare.Web.Tests.Controllers
{
    public class HomeController_Tests: InstaShareWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}