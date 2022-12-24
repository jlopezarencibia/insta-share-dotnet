using Abp.Authorization;
using InstaShare.Authorization.Roles;
using InstaShare.Authorization.Users;

namespace InstaShare.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
