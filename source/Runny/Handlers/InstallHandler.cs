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
            //Log.Debug(String.Format("Server version: {0}", sourcePackage.Version));
            var localPackage = packageManager.LocalRepository.FindPackage(request.ApplicationName);
            //Log.Debug(String.Format("Local version: {0}", localPackage == null ? "--" : localPackage.Version.ToString()));

            if (localPackage == null || localPackage.Version < sourcePackage.Version)
            {
                //Log.Info(String.Format("Installing version {0} for application : {1}", sourcePackage.Version, a.Name));
                packageManager.InstallPackage(request.ApplicationName);
                //RunScript(a);
            }

            return response;
        }
    }
}
