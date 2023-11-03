using Microsoft.AspNetCore.Authorization;

namespace StudentManager.Authentication;

public class ShouldBeAdminRequirementAuthorizationHandler
    : AuthorizationHandler<ShouldBeAdminRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ShouldBeAdminRequirement requirement)
    {
        if (!context.User.HasClaim(x => x.Type == CustomClaimTypes.IS_ADMIN))
            return Task.CompletedTask;

        var isUserAdminClaim = context.User.Claims.First(x => x.Type == CustomClaimTypes.IS_ADMIN).Value;
        bool isUserAnAdmin = bool.Parse(isUserAdminClaim);

        // check if the user    
        if (isUserAnAdmin)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}