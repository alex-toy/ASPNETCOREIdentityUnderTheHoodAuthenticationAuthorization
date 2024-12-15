using Microsoft.AspNetCore.Authorization;

namespace WebAppUnderTheHood.AuthorizationRequirements.HRManagerRequirements;

public class HRManagerProbationRequirement : IAuthorizationRequirement
{
    public int ProbationMonthCount { get; }

    public HRManagerProbationRequirement(int probationMonthCount)
    {
        ProbationMonthCount = probationMonthCount;
    }
}
