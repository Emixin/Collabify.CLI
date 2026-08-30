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
    public void HelpCommand_ShowsHelp()
    {
        var writer = new StringWriter();
        var commandHandler = new CommandHandler();

        Console.SetOut(writer);
        commandHandler.Handle("help");

        Assert.Equal("Showing help ..." + Environment.NewLine, writer.ToString());
    }

    [Fact]
    public void TeamsCommand_GetsTeams()
    {
        var writer = new StringWriter();
        var commandHandler = new CommandHandler();

        Console.SetOut(writer);
        commandHandler.Handle("teams");

        Assert.Equal("Getting teams ..." + Environment.NewLine, writer.ToString());
    }

    [Fact]
    public void EmptyCommand_DoesNothing()
    {
        var writer = new StringWriter();
        var commandHandler = new CommandHandler();

        Console.SetOut(writer);
        commandHandler.Handle("");

        Assert.Equal("", writer.ToString());
    }
}