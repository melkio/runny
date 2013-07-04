using Agatha.ServiceLayer;
using Agatha.ServiceLayer.WCF;
using Agatha.StructureMap;
using Runny.Commands;
using Runny.Handlers;
using System;
using System.ServiceModel;

namespace Runny
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ServiceLayerConfiguration
                (
                    requestHandlersAssembly: typeof(EchoHandler).Assembly,
                    requestsAndResponsesAssembly: typeof(EchoRequest).Assembly,
                    containerImplementation: typeof(Container)
                );
            configuration.Initialize();

            var host = new ServiceHost(typeof(WcfRequestProcessor));
            Console.WriteLine("Apertura host...");
            host.Open();
            Console.WriteLine("Host aperto...");

            Console.ReadLine();
            host.Close();
            Console.WriteLine("Host chiuso...");

            Console.WriteLine("Premere un tasto per continuare...");
            Console.ReadLine();
        }
    }


}
