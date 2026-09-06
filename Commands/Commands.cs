//NOTE: Write commands here

namespace Collabify.CLI;



public class CommandHandler
{

    private readonly APIsClient cookieContainedClient;

    public CommandHandler(APIsClient client)
    {
        cookieContainedClient = client;
    }

    public async Task Handle(string? command)
    {
        switch (command)
        {
            case "login":
                Console.Write("Username: ");
                string username = Console.ReadLine() ?? "";

                Console.Write("Password: ");
                string password = "";

                ConsoleKeyInfo key;
                while (true)
                {
                    key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }

                    password += key.KeyChar;
                }

                var loginUserService = new LoginUserService(cookieContainedClient);
                string message = await loginUserService.LoginUser(username, password);
                Console.WriteLine(message);

                break;

            case "logout":
                Console.WriteLine("logging out ...");
                break;

            case "help":
                Console.WriteLine("Showing help ...");
                break;

            case "teams":
                Console.WriteLine("Getting teams ...");
                break;

            case "exit":
                Console.WriteLine("Exiting ...");
                break;

            case "users":
                Console.WriteLine("Getting users ...");

                //TODO: Use Previous userService created after user login instead of creating new instance.
                UserService userService = new(cookieContainedClient);
                List<string> usersList = await userService.GetUsers();
                Console.WriteLine(string.Join("\n", usersList));
                break;

            case "":
                break;

            default:
                Console.WriteLine("Unknown Command!");
                break;
        }
    }
}