using Microsoft.AspNetCore.Authorization;

namespace StudentManager.Authentication;

public class ShouldBeAdminRequirement : IAuthorizationRequirement
{
    public ShouldBeAdminRequirement()
    {
    }
}