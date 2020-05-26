using IdentityManagement.Infrastructure.Persistence;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManagement.Infrastructure.Services
{
    public class IdentityClaimsProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<AppUser> _claimsFactory;
        private readonly UserManager<AppUser> _userManager;

        public IdentityClaimsProfileService(IUserClaimsPrincipalFactory<AppUser> claimsFactory, UserManager<AppUser> userManager)
        {
            _claimsFactory = claimsFactory;
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            var principal = await _claimsFactory.CreateAsync(user);

            var claims = principal.Claims.ToList();
            claims = claims.Where(f => context.RequestedClaimTypes.Contains(f.Type)).ToList();
            claims.Add(new Claim(JwtClaimTypes.GivenName, user.Name));
            claims.Add(new Claim(JwtClaimTypes.Id, user.Id.ToString()));
            claims.Add(new Claim("userEmailAddress", user.Email));
            claims.Add(new Claim(ClaimTypes.Role, "user"));
            claims.Add(new Claim(JwtClaimTypes.GivenName, user.Name));
            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}