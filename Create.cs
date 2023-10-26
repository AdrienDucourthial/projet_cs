using System;
using System.Diagnostics;

namespace project_cs
{
	public class Create
	{
		public static void Creation(string[] args)
		{
            string project_name = "";
            if (args.Length > 0)
            {
                foreach (string word in args)
                {
                    project_name += word + "_";
                }
            }
            else
            {
                Console.WriteLine("Nom du projet: ");
                project_name = Console.ReadLine()!;
            }

            // TODO: to test
            RunCommand("npm", "install -g create-react-app");
            RunCommand("npx", "create-react-app "+project_name);
            RunCommand("code", "./"+project_name);
        }

        public static void RunCommand(string command, string arguments)
        {
            // Create a process start info
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = command,
                Arguments = arguments,
                RedirectStandardOutput = true, // Capture the command's output
                UseShellExecute = false,      // Required to redirect output
                CreateNoWindow = true         // Do not create a command window
            };
            // Create and start the process
            using (Process process = new Process { StartInfo = startInfo })
            {
                process.Start();

                // Read the command's output
                string output = process.StandardOutput.ReadToEnd();
                if (output != "")
                {
                    Console.WriteLine("Command Output:");
                    Console.WriteLine(output);
                }
            }
        }

    }
}

