using System;
using OpenAI_API;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;

namespace project_cs
{
	public class Translation
	{
        public static bool Translate(string[] args)
		{
            string value = "";
            string langue = "English";
                if (args.Length > 0)
                {
                    if (args[0] == "--langue")
                    {
                        langue = args[1];
                        args = args[2..];
                    }

                    foreach (string word in args)
                    {
                        value += word + " ";
                    }
                    if (value == "") {
                        Console.WriteLine("Error: No value to translate.");
                        return false;
                    }
                    TranslateString(value, langue);
                }
                else
                {
                    Console.WriteLine("Entrez la phrase à traduire: ");
                    value = Console.ReadLine()!;
                    Console.WriteLine("Entrez la langue dans laquelle vous voulez traduire: ");
                    langue = Console.ReadLine()!;
                    TranslateString(value, langue);
                }
            return true;
        }

        private static void TranslateString(string sentence, string language)
        {
            var client = new OpenAIClient();
            var message1 = new OpenAIClient.Message
            {
                role = "system",
                content = "You will be provided with a sentence in French, and your task is to translate it into " + language
            };
            var message2 = new OpenAIClient.Message
            {
                role = "user",
                content = sentence
            };

            client.CallOpenAI(message1, message2).Wait();
        }
    }
}

