using System.Text.Json.Serialization;

namespace Collabify.CLI;


//NOTE: Models are defined here
public class User(string Name, int Age, string Username)
{
    public void UserValidator()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            throw new ArgumentException("name could not be empty!");
        }

        if (int.IsNegative(Age))
        {
            throw new ArgumentOutOfRangeException(nameof(Age));
        }

        if (string.IsNullOrWhiteSpace(Username))
        {
            throw new ArgumentException("username can not be empty!");
        }
    }
}

