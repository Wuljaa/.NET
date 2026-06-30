using System.Security.Claims;

namespace TuristickaAgencija.Mvc.Services
{
    public class TrenutniKorisnikService : ITrenutniKorisnikService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TrenutniKorisnikService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string? GetEmail()
        {
            return _httpContextAccessor.HttpContext?.User.Identity?.Name;
        }

        public bool IsLoggedIn()
        {
            return _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
        }
    }
}

//HC