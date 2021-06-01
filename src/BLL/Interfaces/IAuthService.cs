using System.Threading.Tasks;
using BLL.EtitiesDTO;
using Microsoft.AspNetCore.Identity;

namespace BLL.Interfaces
{
    /// <summary>
    /// Interface for auth service.
    /// </summary>
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
        Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthentication);
    }
}
