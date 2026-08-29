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