using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models;
using IQuality.Models.Authentication;
using IQuality.Models.Forms;
using IQuality.Models.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Raven.Client.Documents.Linq;

namespace IQuality.DomainServices.Services
{
    [Injectable]
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IBuddyRepository _buddyRepository;
        private readonly IConfiguration _config;
        private readonly IInviteService _inviteService;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPatientRepository _patientRepository;
        //private readonly _doctorRepository;

        public AuthenticationService(IConfiguration config, UserManager<ApplicationUser> userManager,
            IBuddyRepository buddyRepository, IPatientRepository patientRepository,
            IInviteService inviteService)
        {
            _config = config;
            _userManager = userManager;
            _buddyRepository = buddyRepository;
            _patientRepository = patientRepository;
            _inviteService = inviteService;
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

        public async Task<(string chatId, ApplicationUser user)> Register(string inviteToken, UserRegister register) 
        {
            if (!await _inviteService.ValidateInvite(inviteToken))
                throw new Exception("Invalid invite provided!");
            
            var invite = await _inviteService.GetInvite(inviteToken);
            
            // Create a new user first
            var applicationUser = await CreateApplicationUser(new ApplicationUser
            {
                UserName =  invite != null ? invite.Email : register.Email,
                Email = invite != null ? invite.Email : register.Email,
                Address = register.Address,
                Name = register.Name,
                EmailConfirmed = true,
                FirstTime = true
            }, register.Password);
            
            var role = invite.InviteType switch
            {
                InviteType.Buddy => Roles.Buddy,
                InviteType.Doctor => Roles.Doctor,
                InviteType.Patient => Roles.Patient,
                InviteType.Admin => Roles.Admin,
                _ => throw new InvalidOperationException()
            };

            await _userManager.AddToRoleAsync(applicationUser, role);
            string chatId = await _inviteService.ConsumeInvite(inviteToken,applicationUser.Id);
            await CreateAdditionalData(invite, applicationUser);
            
            return (chatId: chatId, user: applicationUser);
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
                DateTime.UtcNow.AddDays(Convert.ToDouble(_config["Jwt:ExpireInDays"])),
                credentials
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
        
        public async Task<bool> ApplicationUserExists(string email)
        {
            var foundUser = await _userManager.FindByEmailAsync(email);
            return (foundUser != null);
        } 

        #region Privates

        private async Task<ApplicationUser> CreateApplicationUser(ApplicationUser user, string password)
        {
            var applicationUser = await _userManager.FindByEmailAsync(user.Email);

            // TODO: Implement our own exception
            if (applicationUser != null)
                throw new Exception("User already exists!");

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new Exception($"{result.Errors}");

            return user;
        }


        private static List<Claim> GetValidClaims(ApplicationUser user)
        {
            var options = new IdentityOptions();
            var claims = new List<Claim>
            {
                new Claim(options.ClaimsIdentity.UserIdClaimType, user.Id),
                new Claim(options.ClaimsIdentity.UserNameClaimType, user.Email),
                new Claim("role", user.Roles.First())
            };

            return claims;
        }
        
        private async Task CreateAdditionalData(Invite invite, ApplicationUser applicationUser)
        {
            switch (invite.InviteType)
            {
                case InviteType.Patient:
                    await _patientRepository.SaveAsync(new Patient(applicationUser.Id, invite.InvitedBy));
                    break;
            }
        }

        #endregion
    }
}