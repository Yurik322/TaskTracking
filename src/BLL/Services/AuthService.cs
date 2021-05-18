using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using AutoMapper;
using BLL.EtitiesDTO;
using BLL.Interfaces;
using BLL.JwtFeatures;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtHandler _jwtHandler;
        public AuthService(UserManager<User> userManager, IMapper mapper, JwtHandler jwtHandler)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
        }

        public async Task <AuthResponseDto>Login(UserForAuthenticationDto userForAuthentication)
        {

            var user = await _userManager.FindByNameAsync(userForAuthentication.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
                return (new AuthResponseDto { ErrorMessage = "Invalid Authentication" });
            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, await claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return (new AuthResponseDto { IsAuthSuccessful = true, Token = token });
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<UserForRegistrationDto, User>(userForRegistration);
            return await _userManager.CreateAsync(user, userForRegistration.Password);
        }
    }
}
