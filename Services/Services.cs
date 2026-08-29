using System.Net.Http.Json;

namespace Collabify.CLI;


//NOTE: Define Services here

public class UserService
{
    private string BaseUrl { get; set; } = "http://localhost:8080/APIs/get_users";
    public async Task<List<string>> GetUsers()
    {
        var client = new HttpClient();

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
    public async Task<string> LoginUser(string username, string password)
    {
        var client = new HttpClient();

        var loginrequest = new LoginUserRequest { Username = username, Password = password };

        var response = await client.PostAsJsonAsync(BaseUrl, loginrequest);

        response.EnsureSuccessStatusCode();

        var loginUserResponse = await response.Content.ReadFromJsonAsync<LoginUserResponse>() ?? new LoginUserResponse();

        var message = loginUserResponse.Message ?? "";

        return message;
    }
}


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