//NOTE: Write commands here

namespace Collabify.CLI;



public class CommandHandler
{

    private readonly IAPIsClient cookieContainedClient;

    public CommandHandler(IAPIsClient client)
    {
        cookieContainedClient = client;
    }

    private static string ReadPassword()
    {
        string password = "";

        if (Console.IsInputRedirected)
        {
            password = Console.ReadLine() ?? "";
            return password;
        }

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

        return password;
    }

    public async Task Handle(string? command)
    {
        switch (command)
        {
            case "login":
                Console.Write("Username: ");
                string username = Console.ReadLine() ?? "";

                Console.Write("Password: ");
                var password = ReadPassword();

                var loginUserService = new LoginUserService(cookieContainedClient);
                var message = await loginUserService.LoginUser(username, password);
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

                UserService userService = new(cookieContainedClient);
                var (usersList, statusCode) = await userService.GetUsers();
                Console.WriteLine($"status: {statusCode}");
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
