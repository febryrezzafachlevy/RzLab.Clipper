using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== ChatGPT 5.1 Chatbot Tester ===");
        string apiKey = "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";

        var bot = new ChatbotTest(apiKey);

        while (true)
        {
            Console.Write("\nYou: ");
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
                continue;

            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                break;

            try
            {
                string reply = await bot.AskAsync(input);
                Console.WriteLine("\nGPT-5.1: " + reply);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nError: " + ex.Message);
            }
        }
    }
}
