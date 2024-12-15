using Microsoft.AspNetCore.Authorization;

namespace WebAppUnderTheHood.AuthorizationRequirements.HRManagerRequirements;

public class HRManagerProbationRequirementHandler : AuthorizationHandler<HRManagerProbationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HRManagerProbationRequirement requirement)
    {
        if (!context.User.HasClaim(c => c.Type == "EmploymentDate")) return Task.CompletedTask;

        string employmentDateString = context.User.Claims.FirstOrDefault(c => c.Type == "EmploymentDate")!.Value;

        if (DateTime.TryParse(employmentDateString, out DateTime employmentDate))
        {
            bool hasProbation = employmentDate.AddMonths(requirement.ProbationMonthCount) <= DateTime.Now;
            if (hasProbation) context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
