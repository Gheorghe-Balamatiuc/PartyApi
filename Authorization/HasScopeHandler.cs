using Microsoft.AspNetCore.Authorization;

public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
{
  protected override Task HandleRequirementAsync(
    AuthorizationHandlerContext context,
    HasScopeRequirement requirement
  ) {
    // If user does not have the scope claim, get out of here
    if (!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == requirement.Issuer))
      return Task.CompletedTask;

    // Split the scopes string into an array
    var scopeClaim = context.User
      .FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer);

    if (scopeClaim == null)
      return Task.CompletedTask;
      
    var scopes = scopeClaim.Value.Split(' ');

    // Succeed if the scope array contains the required scope
    if (scopes.Any(s => s == requirement.Scope))
      context.Succeed(requirement);

    return Task.CompletedTask;
  }
}