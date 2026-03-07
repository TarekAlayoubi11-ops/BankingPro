using Microsoft.AspNetCore.Authorization;

namespace BankingPro.Authorization
{
    public class UserOwnerOrAdminRequirement : IAuthorizationRequirement
    {
    }
}
