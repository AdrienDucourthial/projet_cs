using System;
using OpenAI_API;
using System.Text;
using System.Net.Http;

namespace project_cs
{
	public class Translation
	{
        public static void Translate(string[] args)
		{
            string value = "";
            if (args.Length > 0)
            {
                foreach (string word in args)
                {
                    value += word + " ";
                }
                TranslateString(value);
            }
            else
            {
                Console.WriteLine("Entrez la phrase à traduire: ");
                value = Console.ReadLine()!;
                TranslateString(value);
            }

        }

        private static void TranslateString(string sentence)
        {
            var client = new OpenAIClient();
            client.Translate(sentence).Wait();
        }
    }
}

