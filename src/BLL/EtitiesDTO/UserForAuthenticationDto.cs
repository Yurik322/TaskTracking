using System.ComponentModel.DataAnnotations;

namespace BLL.EtitiesDTO
{
    /// <summary>
    /// Data transfer object for user authentication model.
    /// </summary>
    public class UserForAuthenticationDto
    {

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
