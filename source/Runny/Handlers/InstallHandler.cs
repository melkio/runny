using Agatha.Common;
using Agatha.ServiceLayer;
using NuGet;
using Runny.Commands;

namespace Runny.Handlers
{
    class InstallHandler : RequestHandler<InstallRequest, InstallResponse>
    {
        public override Response Handle(InstallRequest request)
        {
            var response = CreateTypedResponse();

            var source = PackageRepositoryFactory.Default.CreateRepository(request.NugetSource);
            var packageManager = new PackageManager(source, request.Location);

            var sourcePackage = packageManager.SourceRepository.FindPackage(request.ApplicationName);
            var localPackage = packageManager.LocalRepository.FindPackage(request.ApplicationName);

            if (localPackage == null || localPackage.Version < sourcePackage.Version)
            {
                packageManager.InstallPackage(request.ApplicationName);
                localPackage.ExecuteInitIfExists(request.Location);
                localPackage.ExecuteInstallIfExists(request.Location);
            }

            return response;
        }
    }
}
