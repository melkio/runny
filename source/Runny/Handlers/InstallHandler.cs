using Agatha.Common;
using Agatha.ServiceLayer;
using Common.Logging;
using NuGet;
using Runny.Commands;

namespace Runny.Handlers
{
    class InstallHandler : RequestHandler<InstallRequest, InstallResponse>
    {
        static ILog Log = LogManager.GetCurrentClassLogger();

        public override Response Handle(InstallRequest request)
        {
            Log.Info(m => m("Handling request to install application: {0}", request));

            var response = CreateTypedResponse();

            var source = PackageRepositoryFactory.Default.CreateRepository(request.NugetSource);
            var packageManager = new PackageManager(source, request.Location);

            var sourcePackage = packageManager.SourceRepository.FindPackage(request.ApplicationName);
            Log.Debug(m => m("Source package version: {0}", sourcePackage.Version));
            var localPackage = packageManager.LocalRepository.FindPackage(request.ApplicationName);
            Log.Debug(m => m("Local package version: {0}", sourcePackage.Version));

            if (localPackage == null || localPackage.Version < sourcePackage.Version)
            {
                packageManager.InstallPackage(request.ApplicationName);
                localPackage.ExecuteInitIfExists(request.Location);
                localPackage.ExecuteInstallIfExists(request.Location);
            }

            Log.Info(m => m("Handled request to install application: {0}", request));

            return response;
        }
    }
}
