using System;

namespace project_cs
{
	public class Correction
	{
		public static void Correct(string[] args)
		{
			if (args.Length > 0)
			{
				string result = "";
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
            client.Correct(sentence).Wait();
        }
	}
}

