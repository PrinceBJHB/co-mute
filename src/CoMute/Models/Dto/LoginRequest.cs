
namespace CoMute.Web.Models.Dto
{
    public sealed class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string grant_type { get; set; }
    }
}
