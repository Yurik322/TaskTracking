namespace BLL.EtitiesDTO
{
    /// <summary>
    /// Data transfer object for auth response model.
    /// </summary>
    public class AuthResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }
}
