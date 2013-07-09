using Agatha.Common;
using Agatha.ServiceLayer;
using Common.Logging;
using NuGet;
using Runny.Commands;

namespace Runny.Handlers
{
    class GetLastestVersionHandler : RequestHandler<GetLastVersionRequest, GetLastVersionResponse>
    {
        static ILog Log = LogManager.GetCurrentClassLogger();

        public override Response Handle(GetLastVersionRequest request)
        {
            Log.Info(m => m("Handling request to retrieve last application version: {0}", request));

            var response = CreateTypedResponse();

            var source = PackageRepositoryFactory.Default.CreateRepository(request.NugetSource);

            var sourcePackage = source.FindPackage(request.ApplicationName);
            Log.Debug(m => m("Source package version: {0}", sourcePackage.Version));

            response.Version = sourcePackage.Version.ToString();
            return response;
        }
    }
}
