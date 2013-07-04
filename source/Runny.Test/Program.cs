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

            var dispatcher = IoC.Container.Resolve<IRequestDispatcher>();
            var response = dispatcher.Get<EchoResponse>(new EchoRequest { Message = "Hi melkio" });
            Console.WriteLine(response.Message);

            Console.ReadLine();
        }
    }
}
