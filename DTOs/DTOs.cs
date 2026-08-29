//NOTE: Write DTOs here


using System.Text.Json.Serialization;

namespace Collabify.CLI;

public class GetUsersResponse
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("users")]
    public List<string>? UsersList { get; set; }

    [JsonPropertyName("usersCount")]
    public int UsersCount { get; set; }
}


public class LoginUserRequest
{
    [JsonPropertyName("username")]
    public required string Username { get; set; }

    [JsonPropertyName("password")]
    public required string Password { get; set; }
}


public class LoginUserResponse
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}


public class CreateTeamRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("maxMembers")]
    public int MaxMembers { get; set; }
}


public class CreateTeamResponse
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}