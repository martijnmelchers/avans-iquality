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
    public class UserService: IUserService
    {

        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<List<ApplicationUser>> GetUsers()
        {
            return await _userRepository.GetAllWhereAsync((item) => item.Email != null);
        }

        public async Task<ApplicationUser> GetUser(string applicationUserId)
        {
            return await _userRepository.GetByIdAsync(applicationUserId);
        }
        
        public async Task DeactivateUser(string applicationUserId)
        {
            ApplicationUser user = await _userRepository.GetByIdAsync(applicationUserId);
            user.Active = false;
            await _userRepository.SaveAsync(user);
        }
        
        public async Task DeleteUser(string applicationUserId)
        {
            ApplicationUser user = await _userRepository.GetByIdAsync(applicationUserId);
            _userRepository.Delete(user);
        }
    }
}