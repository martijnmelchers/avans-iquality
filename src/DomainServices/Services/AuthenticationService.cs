using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories;
using IQuality.Infrastructure.Database.Repositories.Interface;
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
        private readonly IInviteRepository _inviteRepository;

        public AuthenticationService(IConfiguration config, UserManager<ApplicationUser> userManager, IInviteRepository inviteRepository)
        {
            _config = config;
            _userManager = userManager;
            _inviteRepository = inviteRepository;
        }

        public async Task<(bool success, ApplicationUser user)> Login(string email, string password)
        {
            var applicationUser = await _userManager.FindByEmailAsync(email);

            if (applicationUser == null)
                return (false, null);

            // Returns whether or not the user has confirmed their email
            if (!await _userManager.IsEmailConfirmedAsync(applicationUser))
                return (false, null);

            // (Could be removed?) Returns whether or not the use ris locked out
            if (await _userManager.IsLockedOutAsync(applicationUser))
                return (false, null);

            return (await _userManager.CheckPasswordAsync(applicationUser, password), applicationUser);
        }

        public async Task<ApplicationUser> Register(ApplicationUser user, string password)
        {
            var applicationUser = await _userManager.FindByEmailAsync(user.Email);

            // TODO: Implement our own exception
            if(applicationUser != null)
                throw new Exception("User already exists!");
            
            var result = await _userManager.CreateAsync(user, password);
            
            applicationUser = await _userManager.FindByEmailAsync(user.Email);
            
            return applicationUser;
        }

        public string GenerateToken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:AudienceSecret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:AudienceId"],
                GetValidClaims(user),
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireTimeInMinutes"])),
                credentials
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }

        private static List<Claim> GetValidClaims(ApplicationUser user)
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
        
        public async void CreateInvite(string userId)
        {
            var invite = new Invite
            {
                ApplicationUserId = userId,
                Used = false
            };
            
            await _inviteRepository.SaveAsync(invite);
        }

        public async Task<Invite> GetInvite(string id)
        {
            return await _inviteRepository.GetByIdAsync(id);
        }

        // Uses the invite link.
        public async void RespondInvite(Invite link, bool accepted = true)
        {
            if (accepted)
            {
                link.Used = true;
                await _inviteRepository.SaveAsync(link);
            }
            else
            {
                _inviteRepository.DeleteAsync(link);
            }
        }
    }
}
