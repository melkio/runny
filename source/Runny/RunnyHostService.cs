using Agatha.ServiceLayer;
using Agatha.ServiceLayer.WCF;
using Agatha.StructureMap;
using Runny.Commands;
using Runny.Handlers;
using System;
using System.ServiceModel;
using Topshelf;

namespace Runny
{
    class RunnyHostService 
    {
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
            _host.Open();
        }

        public void Stop()
        {
            _host.Close();
        }
    }
}
