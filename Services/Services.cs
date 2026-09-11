using System.Buffers.Text;
using System.Globalization;
using System.Net;
using System.Net.Http.Json;

namespace Collabify.CLI;


//NOTE: Defined a class responsible for managing cookies
//TODO: Define an interface to follow abstraction and dependency inversion principles
public class APIsClient : IAPIsClient
{
    private readonly HttpClientHandler HttpClientHandlerWithCookie = new()
    {
        CookieContainer = new CookieContainer()
    };

    private readonly HttpClient client;

    public APIsClient()
    {
        client = new HttpClient(HttpClientHandlerWithCookie);
    }

    public async Task<(string, HttpStatusCode?)> LoginUserAPI(string username, string password, string? BaseUrl)
    {
        var loginrequest = new LoginUserRequest { Username = username, Password = password };

        HttpResponseMessage response;

        try
        {
            response = await client.PostAsJsonAsync(BaseUrl, loginrequest);
        }

        catch (HttpRequestException)
        {
            return ("Connection failed!", null);
        }

        if (!response.IsSuccessStatusCode)
        {
            return ("Login failed!", response.StatusCode);
        }

        var loginUserResponse = await response.Content.ReadFromJsonAsync<LoginUserResponse>() ?? new LoginUserResponse();

        var message = loginUserResponse.Message ?? "";

        return (message, HttpStatusCode.OK);
    }

    public async Task<(List<string>, HttpStatusCode?)> GetUsersAPI(string? BaseUrl)
    {
        HttpResponseMessage response;
        try
        {
            response = await client.GetAsync(BaseUrl);
        }

        catch (HttpRequestException)
        {
            List<string> emptyList = new();
            return (emptyList, null);
        }

        if (!response.IsSuccessStatusCode)
        {
            List<string> emptyList = new();
            return (emptyList, response.StatusCode);
        }

        var getUserResponse = await response.Content.ReadFromJsonAsync<GetUsersResponse>() ?? new GetUsersResponse();

        var usersList = getUserResponse.UsersList ?? [];

        return (usersList, response.StatusCode);
    }

}


public class LoginUserService
{
    private readonly IAPIsClient httpClientContainer;

    public LoginUserService(IAPIsClient client)
    {
        httpClientContainer = client;
    }

    public async Task<(string, HttpStatusCode?)> LoginUser(string username, string password)
    {
        var baseUrl = Environment.GetEnvironmentVariable("COLLABIFY_API_URL") ?? "";
        var messageStatusCodeContained = await httpClientContainer.LoginUserAPI(username, password, baseUrl + "/login");
        return messageStatusCodeContained;
    }
}


public class UserService
{
    private readonly IAPIsClient httpClientContainer;

    public UserService(IAPIsClient client)
    {
        httpClientContainer = client;
    }

    public async Task<(List<string>, HttpStatusCode?)> GetUsers()
    {
        var baseUrl = Environment.GetEnvironmentVariable("COLLABIFY_API_URL") ?? "";
        var usersListStatusCodeContained = await httpClientContainer.GetUsersAPI(baseUrl + "/get_users");
        return usersListStatusCodeContained;
    }
}


//TODO: Complete it later.
public class CreateTeamservice
{
    public async Task<string> CreateTeam(string name, int maxMembers)
    {
        var client = new HttpClient();

        var createTeamRequest = new CreateTeamRequest { Name = name, MaxMembers = maxMembers };

        var baseUrl = Environment.GetEnvironmentVariable("COLLABIFY_API_URL") ?? "";
        var response = await client.PostAsJsonAsync(baseUrl + "/create_team", createTeamRequest);

        response.EnsureSuccessStatusCode();

        var createTeamResponse = await response.Content.ReadFromJsonAsync<CreateTeamResponse>() ?? new CreateTeamResponse();

        var messsage = createTeamResponse.Message ?? "";

        return messsage;
    }
}