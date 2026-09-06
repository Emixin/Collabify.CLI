using System.Buffers.Text;
using System.Net;
using System.Net.Http.Json;

namespace Collabify.CLI;


//NOTE: Defined a class for storing cookies.
public class APIsClient
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

    public async Task<string> LoginUserAPI(string username, string password, string BaseUrl)
    {
        var loginrequest = new LoginUserRequest { Username = username, Password = password };

        var response = await client.PostAsJsonAsync(BaseUrl, loginrequest);

        response.EnsureSuccessStatusCode();

        var loginUserResponse = await response.Content.ReadFromJsonAsync<LoginUserResponse>() ?? new LoginUserResponse();

        var message = loginUserResponse.Message ?? "";

        return message;
    }

    public async Task<List<string>> GetUsersAPI(string BaseUrl)
    {
        var response = await client.GetAsync(BaseUrl);

        response.EnsureSuccessStatusCode();

        var getUserResponse = await response.Content.ReadFromJsonAsync<GetUsersResponse>() ?? new GetUsersResponse();

        var usersList = getUserResponse.UsersList ?? [];

        return usersList;
    }

}


public class LoginUserService
{
    private string BaseUrl { get; set; } = "http://localhost:8080/APIs/login";

    private readonly APIsClient httpClientContainer;

    public LoginUserService(APIsClient client)
    {
        httpClientContainer = client;
    }

    public async Task<string> LoginUser(string username, string password)
    {
        var message = await httpClientContainer.LoginUserAPI(username, password, BaseUrl);
        return message;
    }
}


//FIX: GetUsers returns 401 even for a logged in user!
public class UserService
{
    private string BaseUrl { get; set; } = "http://localhost:8080/APIs/get_users";

    private readonly APIsClient httpClientContainer;

    public UserService(APIsClient client)
    {
        httpClientContainer = client;
    }

    public async Task<List<string>> GetUsers()
    {
        var usersList = await httpClientContainer.GetUsersAPI(BaseUrl);
        return usersList;
    }
}


//TODO: Complete it later.
public class CreateTeamservice
{
    private string BaseUrl { get; set; } = "http://localhost:8080/APIs/create_team";

    public async Task<string> CreateTeam(string name, int maxMembers)
    {
        var client = new HttpClient();

        var createTeamRequest = new CreateTeamRequest { Name = name, MaxMembers = maxMembers };

        var response = await client.PostAsJsonAsync(BaseUrl, createTeamRequest);

        response.EnsureSuccessStatusCode();

        var createTeamResponse = await response.Content.ReadFromJsonAsync<CreateTeamResponse>() ?? new CreateTeamResponse();

        var messsage = createTeamResponse.Message ?? "";

        return messsage;
    }
}