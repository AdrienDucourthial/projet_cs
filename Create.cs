using System;
using System.Diagnostics;
using Google.Cloud.Translation.V2;
using Newtonsoft.Json.Linq;

namespace project_cs
{
	public class Create
	{
		public static bool Creation(string[] args)
		{
            string project_name = "";
            string techno = "React";
            if (args.Length > 0)
            {
                if (args[0] == "--project")
                {
                    techno = args[1];
                    args = args[2..];
                }

                foreach (string word in args)
                {
                    project_name += word + "_";
                }

                if (project_name == "")
                {
                    Console.WriteLine("Error: No project name.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Nom du projet: ");
                project_name = Console.ReadLine()!;
                Console.WriteLine("Technologie: ");
                techno = Console.ReadLine()!;
            }

            switch (techno)
            {
                case "React":
                    RunCommand("npm", "install -g create-react-app");
                    RunCommand("npx", "create-react-app " + project_name);
                    break;
                case "Angular":
                    RunCommand("npm", "install -g @angular/cli");
                    RunCommand("ng", "new " + project_name);
                    break;
                case "Vue":
                    RunCommand("npm", "install -g vue-cli");
                    Console.WriteLine("Due to some small issue, you need to press enter 12 times.");
                    RunCommand("vue", "init webpack " + project_name);
                    break;
                default:
                    Console.WriteLine("Error: Technologies not recognized.");
                    return false;
            }
            RunCommand("code", "./" + project_name);

            return true;
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

