using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories;
using IQuality.Models;
using IQuality.Models.Authentication;
using IQuality.Models.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IQuality.DomainServices.Services
{
    [Injectable]
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RegistrationLinkRepository _registrationLinkRepository;

        public AuthenticationService(IConfiguration config, UserManager<ApplicationUser> userManager, RegistrationLinkRepository registrationLinkRepository)
        {
            _config = config;
            _userManager = userManager;
            _registrationLinkRepository = registrationLinkRepository;
        }

        public async Task<(bool success, ApplicationUser user)> Login(string email, string password)
        {
            var applicationUser = await _userManager.FindByEmailAsync(email);

            // Returns whether or not the user has confirmed their email
            if (!await _userManager.IsEmailConfirmedAsync(applicationUser))
                return (false, null);

            // (Could be removed?) Returns whether or not the use ris locked out
            if (await _userManager.IsLockedOutAsync(applicationUser))
                return (false, null);

            return (await _userManager.CheckPasswordAsync(applicationUser, password), applicationUser);
        }

        public string GenerateToken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:AudienceSecret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["AppSettings:Jwt:Issuer"],
                _config["Jwt:AudienceId"],
                GetValidClaims(user),
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireTimeInMinutes"])),
                credentials
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }

        private List<Claim> GetValidClaims(ApplicationUser user)
        {
            var options = new IdentityOptions();
            var claims = new List<Claim>
            {
                new Claim(options.ClaimsIdentity.UserIdClaimType, user.Id),
                new Claim(options.ClaimsIdentity.UserNameClaimType, user.Email)
            };

            claims.AddRange(user.Claims.Where(c => c.ClaimType == ClaimTypes.Role)
                .Select(c => new Claim("roles", c.ClaimValue)));

            return claims;
        }

        public async void CreateInvite(RegistrationLink link)
        {
            await _registrationLinkRepository.SaveAsync(link);
        }

        public async Task<RegistrationLink> GetInvite(string id)
        {
            return await _registrationLinkRepository.GetByIdAsync(id);
        }

        // Uses the invite link.
        public async void RespondInvite(RegistrationLink link, bool accepted = true)
        {
            if (accepted)
            {
                link.Used = true;
                await _registrationLinkRepository.SaveAsync(link);
            }
            else
            {
                _registrationLinkRepository.DeleteAsync(link);
            }
        }
    }
}
