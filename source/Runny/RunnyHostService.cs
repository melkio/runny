using Agatha.ServiceLayer;
using Agatha.ServiceLayer.WCF;
using Agatha.StructureMap;
using Common.Logging;
using Runny.Commands;
using Runny.Handlers;
using System;
using System.ServiceModel;
using Topshelf;

namespace Runny
{
    class RunnyHostService 
    {
        static ILog Log = LogManager.GetCurrentClassLogger();

        readonly ServiceHost _host;

        public RunnyHostService()
        {
            var configuration = new ServiceLayerConfiguration
                (
                    requestHandlersAssembly: typeof(EchoHandler).Assembly,
                    requestsAndResponsesAssembly: typeof(EchoRequest).Assembly,
                    containerImplementation: typeof(Container)
                );
            configuration.Initialize();

            _host = new ServiceHost(typeof(WcfRequestProcessor));
        }

        public void Start()
        {
            Log.Info(m => m("Attempting to start service to host runny..."));
            _host.Open();
            Log.Info(m => m("Started service that host runny"));
        }

        public void Stop()
        {
            Log.Info(m => m("Attempting to stop service that host runny..."));
            _host.Close();
            Log.Info(m => m("Stopped service that host runny"));
        }
    }
}
