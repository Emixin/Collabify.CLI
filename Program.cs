namespace Collabify.CLI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //NOTE: The Program entry is here

            DotNetEnv.Env.Load();

            string? input;

            var commandHandler = new CommandHandler(new APIsClient());

            do
            {
                Console.Write("collabify> ");

                input = Console.ReadLine();

                await commandHandler.Handle(input);
                
            } while (input != "exit");
        }
    }
}
