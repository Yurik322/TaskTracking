using System.Collections.Generic;

namespace BLL.EtitiesDTO
{
    /// <summary>
    /// Data transfer object for registration response model.
    /// </summary>
    public class RegistrationResponseDto
    {
        public bool IsSuccessfulRegistration { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
