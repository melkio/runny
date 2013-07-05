using NuGet;
using System;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace Runny
{
    static class PackageExtensionMethods
    {
        public static void ExecutePowershellScriptFileIfExists(this IPackage package, String scriptName, String locationRoot)
        {
            var script = package
                .GetToolFiles()
                .Where(f => Path.GetExtension(f.Path) == ".ps1")
                .SingleOrDefault(f => Path.GetFileNameWithoutExtension(f.Path) == scriptName);

            if (script == null) return;

            var installPath = Path.Combine(locationRoot, String.Format("{0}.{1}", package.Id, package.Version));
            var scriptPath = Path.Combine(installPath, script.Path);
            var toolsPath = Path.GetDirectoryName(scriptPath);

            var runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();

            var setExecutionPolicy = new RunspaceInvoke();
            setExecutionPolicy.Invoke("Set-ExecutionPolicy -ExecutionPolicy Bypass -Scope Process");

            var pipeline = runspace.CreatePipeline();
            var command = new Command(scriptPath);
            command.Parameters.Add("installPath", installPath);
            command.Parameters.Add("toolsPath", toolsPath);
            command.Parameters.Add("package", package);
            pipeline.Commands.Add(command);

            pipeline.Invoke();

            runspace.Close();
        }

        public static void ExecuteInitIfExists(this IPackage package, String locationRoot)
        {
            package.ExecutePowershellScriptFileIfExists("init", locationRoot);
        }

        public static void ExecuteInstallIfExists(this IPackage package, String locationRoot)
        {
            package.ExecutePowershellScriptFileIfExists("install", locationRoot);
        }

        public static void ExecuteUninstallIfExists(this IPackage package, String locationRoot)
        {
            package.ExecutePowershellScriptFileIfExists("uninstall", locationRoot);
        }
    }
}
