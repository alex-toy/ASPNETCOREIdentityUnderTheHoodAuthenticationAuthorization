using Microsoft.AspNetCore.Authorization;
using WebAppUnderTheHood.AuthorizationRequirements.HRManagerRequirements;

namespace WebAppUnderTheHood.AuthorizationRequirements;

public class PermissionHandler : IAuthorizationHandler
{
    public Task HandleAsync(AuthorizationHandlerContext context)
    {
        List<IAuthorizationRequirement> pendingRequirements = context.PendingRequirements.ToList();

        foreach (var requirement in pendingRequirements)
        {
            if (requirement is HRManagerProbationRequirement hRManagerProbationRequirement)
            {
                return hRManagerProbationRequirement.HandleAsync(context);
            }
        }

        return Task.CompletedTask;
    }
}
