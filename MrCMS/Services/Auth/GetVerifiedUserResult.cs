using System.Linq;
using System.Threading.Tasks;
using MrCMS.Entities.People;
using MrCMS.Models.Auth;
using MrCMS.Settings;
using MrCMS.Website;

namespace MrCMS.Services.Auth
{
    public class GetVerifiedUserResult : IGetVerifiedUserResult
    {
        private readonly AuthSettings _authSettings;
        private readonly AuthRoleSettings _roleSettings;

        public GetVerifiedUserResult(AuthSettings authSettings, AuthRoleSettings roleSettings)
        {
            _authSettings = authSettings;
            _roleSettings = roleSettings;
        }

        public LoginResult GetResult(User user, string returnUrl)
        {
            if (!user.IsActive)
                return new LoginResult { Status = LoginStatus.LockedOut, Message = "User is locked out." };

            var redirectUrl = returnUrl ?? (user.IsAdmin ? "~/admin" : "~");

            if (_authSettings.TwoFactorAuthEnabled && user.Roles.Any(role => _roleSettings.TwoFactorAuthRoles.Contains(role.Id)))
                return new LoginResult
                {
                    User = user,
                    Status = LoginStatus.TwoFactorRequired,
                    ReturnUrl = redirectUrl
                };

            return new LoginResult
            {
                User = user,
                Status = LoginStatus.Success,
                ReturnUrl = redirectUrl
            };
        }
    }
}