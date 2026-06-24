namespace TuristickaAgencija.Mvc.Services
{
    public interface ITrenutniKorisnikService
    {
        string? GetUserId();
        string? GetEmail();
        bool IsLoggedIn();

    }
}