using System.Net;
using Xunit;


namespace Collabify.CLI;

//NOTE: Write Tests here
public class UserTests
{
    [Fact]
    public void InvalidName_ThrowsException()
    {
        var user = new User("", 25, "user1");

        Assert.Throws<ArgumentException>(
            () => user.UserValidator()
            );
    }

    [Fact]
    public void InvalidAge_ThrowsException()
    {
        var user = new User("User1", -1, "user1");

        Assert.Throws<ArgumentOutOfRangeException>(
                () => user.UserValidator()
            );
    }

    [Fact]
    public void InvalidUsername_ThrowsException()
    {
        var user = new User("User1", 25, "");

        Assert.Throws<ArgumentException>(
                () => user.UserValidator()
            );
    }
}


public class CommandHandlerTests
{
    [Fact]
    public async Task HelpCommand_ShowsHelp()
    {
        var writer = new StringWriter();
        var commandHandler = new CommandHandler(new APIsClient());

        Console.SetOut(writer);
        await commandHandler.Handle("help");

        Assert.Equal("Showing help ..." + Environment.NewLine, writer.ToString());
    }

    [Fact]
    public async Task TeamsCommand_GetsTeams()
    {
        var writer = new StringWriter();
        var commandHandler = new CommandHandler(new APIsClient());

        Console.SetOut(writer);
        await commandHandler.Handle("teams");

        Assert.Equal("Getting teams ..." + Environment.NewLine, writer.ToString());
    }

    [Fact]
    public async Task EmptyCommand_DoesNothing()
    {
        var writer = new StringWriter();
        var commandHandler = new CommandHandler(new APIsClient());

        Console.SetOut(writer);
        await commandHandler.Handle("");

        Assert.Equal("", writer.ToString());
    }

    [Fact]
    public async Task LoginCommand_Works()
    {
        Console.SetIn(new StringReader("user\npassword"));

        var writer = new StringWriter();
        Console.SetOut(writer);

        var commandHandler = new CommandHandler(new FakeAPIsClient());
        await commandHandler.Handle("login");

        Assert.Contains("Logged in successfully!", writer.ToString());
    }
}


//NOTE: Defined a fake APIs client to test without running go server!
public class FakeAPIsClient : IAPIsClient
{
    public async Task<(string, HttpStatusCode?)> LoginUserAPI(string username, string password, string? BaseUrl = null)
    {
        var loginrequest = new LoginUserRequest { Username = username, Password = password };
        
        return ("Logged in successfully!", HttpStatusCode.OK);
    }
    public async Task<(List<string>, HttpStatusCode?)> GetUsersAPI(string? BaseUrl = null)
    {
        List<string> emptyList = new();
        return (emptyList, HttpStatusCode.OK);
    }
}