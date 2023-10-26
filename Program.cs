﻿using System.Reflection;

namespace microsoft.botsay;

internal class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            var versionString = Assembly.GetEntryAssembly()?
                                    .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
                                    .InformationalVersion
                                    .ToString();

            Console.WriteLine($"clitool v{versionString}");
            Console.WriteLine("-------------");
            Console.WriteLine("\nUsage:");
            Console.WriteLine(" -c      Corrige les fautes d'orthographe");
            Console.WriteLine(" -t      Traduis en anglais");
            Console.WriteLine(" create  Créée une application React et installe les dependances");
            return;
        }

        CallFunctions(args);

    }

    static void CallFunctions(string[] args)
    {
        bool tryAgain = false;
        switch (args[0])
        {
            case "-c":
                project_cs.Correction.Correct(args[1..]);
                tryAgain = true;
                break;
            case "-t":
                project_cs.Translation.Translate(args[1..]);
                tryAgain = true;
                break;
            case "create":
                project_cs.Create.Creation(args[1..]);
                tryAgain = true;
                break;
            default:
                Console.WriteLine("Not recognized command");
                break;
        }

        if (tryAgain) { 
            Console.WriteLine("Try again ?");
            var resp = Console.ReadLine();
            if (resp == "y" || resp == "Y" || resp == "yes" || resp == "Yes" || resp == "YES")
            {
                string[] newargs = { args[0] };
                CallFunctions(newargs);
            }
        }
    }
}

