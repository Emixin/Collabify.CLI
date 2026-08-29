//NOTE: Write commands here

namespace Collabify.CLI;



public class CommandHandler
{
    public void Handle(string? command)
    {
        switch (command)
        {
            case "help":
                Console.WriteLine("Showing help ...");
                break;

            case "teams":
                Console.WriteLine("Getting teams ...");
                break;

            case "exit":
                Console.WriteLine("Exiting ...");
                break;

            case "":
                break;

            default:
                Console.WriteLine("Unknown Command!");
                break;
        }
    }
}