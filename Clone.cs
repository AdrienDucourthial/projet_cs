using System;
using static OpenAIClient;
using static System.Net.Mime.MediaTypeNames;

namespace project_cs
{
	public class Clone
	{
		public static void Cloning(string[] args)
		{
			string source = "";
			if (args.Length == 1)
			{
				source = args[0];
			}
			else if (args.Length == 0)
			{
				Console.WriteLine("Entrez l'url du repo: ");
				source = Console.ReadLine()!;
      }
      else {
        Console.WriteLine("Error: Too many arguments were given");
        return;
      }

      Create.RunCommand("git", "clone "+source);
      string[] url_splitted = source.Split('/');
      string proj_name = url_splitted[url_splitted.Length-1];
      Create.RunCommand("code", proj_name);
    }
  }
}

