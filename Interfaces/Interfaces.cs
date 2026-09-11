using System.Net;

namespace Collabify.CLI;

public interface IAPIsClient
{
    Task<(string, HttpStatusCode?)> LoginUserAPI(string username, string password, string? BaseUrl);
    Task<(List<string>, HttpStatusCode?)> GetUsersAPI(string? BaseUrl);
    }
