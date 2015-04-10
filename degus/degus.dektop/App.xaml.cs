﻿namespace Degus
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;

    public partial class App : Application
    {

        public App()
        {
            Startup += App_Startup;
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            var parameterSet = ParameterParser.GetScriptingConfiguration(e.Args);
            if (!parameterSet.IsValid)
            {
                // TODO: implement more informative error reporting.
                Console.WriteLine("Invalid parameters");
                this.Shutdown((int)DegusExitCode.InvalidParameters);
            }
            else
            {
                var scripter = new SqlServer2012Scripter();
                switch (parameterSet.Action)
                {
                    case ScriptingAction.ShowGUI:
                        MainWindow wnd = new MainWindow();
                        wnd.Show();
                        break;
                    case ScriptingAction.Script:
                        scripter.WriteScript(parameterSet.ServerName, parameterSet.DatabaseName, parameterSet.UseSingleFile, parameterSet.OutputPath, parameterSet.OverwriteFile, parameterSet.ScriptData);
                        this.Shutdown((int)DegusExitCode.Success);
                        break;
                    case ScriptingAction.ScriptFromConfig:
                        scripter.WriteScript(parameterSet.ServerName, parameterSet.DatabaseName, parameterSet.UseSingleFile, parameterSet.OutputPath, parameterSet.OverwriteFile, parameterSet.ScriptData);
                        this.Shutdown((int)DegusExitCode.Success);
                        break;
                    default:
                        Console.WriteLine("Unknown action");
                        this.Shutdown((int)DegusExitCode.InvalidParameters);
                        break;
                }
            }
        }
    }
}
