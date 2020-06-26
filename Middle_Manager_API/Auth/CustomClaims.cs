using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Internal;

namespace Middle_Manager_API.Auth
{
    public class CustomClaims : IAuthorizationRequirement
    {
        public CustomClaims(string claimType)
        {
            ClaimType = claimType;
        }

        public string ClaimType { get; }
    }

    public class CustomRequireClaimHandler : AuthorizationHandler<CustomClaims>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CustomClaims requirement)
        {
            var hasClaim = context.User.Claims.Any(t => t.Type == requirement.ClaimType);
            if (hasClaim)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public static class AuthorizationPolicyBuilderExtensions
    {
        public static AuthorizationPolicyBuilder RequireCustomClaim(this AuthorizationPolicyBuilder builder, string claimType)
        {
            builder.AddRequirements(new CustomClaims(claimType));
            return builder;
        }
    }
}
