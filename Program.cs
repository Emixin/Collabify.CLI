namespace Collabify.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //NOTE: The Program entry is here

            string? input;

            var commandHandler = new CommandHandler();

            do
            {
                Console.Write("collabify> ");

                input = Console.ReadLine();

                commandHandler.Handle(input);

            } while (input != "exit");
        }
    }
}
