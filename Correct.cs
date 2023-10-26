using System;
using static OpenAIClient;
using static System.Net.Mime.MediaTypeNames;

namespace project_cs
{
	public class Correction
	{
		public static void Correct(string[] args)
		{
			string result = "";
			if (args.Length > 0)
			{
				foreach (string word in args)
				{
					result += word + " ";
				}
				CorrectString(result);
			}
			else
			{
				Console.WriteLine("Entrez la phrase à corriger: ");
				string value = Console.ReadLine()!;
                CorrectString(value);
            }
		}

		private static void CorrectString(string sentence)
		{
            var client = new OpenAIClient();
            var message1 = new Message
            {
                role = "system",
                content = "You will be provided with statements, and your task is to convert them to standard French."
            };
            var message2 = new Message
            {
                role = "user",
                content = sentence
            };
            client.CallOpenAI(message1, message2).Wait();
        }
	}
}

