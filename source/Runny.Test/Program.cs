using Agatha.Common;
using Agatha.Common.InversionOfControl;
using Agatha.StructureMap;
using Runny.Commands;
using System;

namespace Runny.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ClientConfiguration
                (
                    requestsAndResponsesAssembly: typeof(EchoRequest).Assembly,
                    containerImplementation: typeof(Container)
                );
            configuration.Initialize();

            var factory = IoC.Container.Resolve<IRequestDispatcherFactory>();

            using (var dispatcher = factory.CreateRequestDispatcher())
            {
                var response = dispatcher.Get<EchoResponse>(new EchoRequest { Message = "Hi melkio" });
                Console.WriteLine(response.Message);
            }

            using (var dispatcher = factory.CreateRequestDispatcher())
            {
                var request = new InstallRequest
                {
                    ApplicationName ="LittleJohn",
                    Location = @"D:\temp\LittleJohn",
                    NugetSource = "http://192.168.1.147:10016/nuget"
                };
                var response = dispatcher.Get<InstallResponse>(request);
                Console.WriteLine("installazione terminata");
            }

            Console.ReadLine();
        }
    }
}
