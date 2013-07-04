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
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>                                 
            {
                x.Service<RunnyHostService>(s => 
                        {
                            s.ConstructUsing(name => new RunnyHostService());
                            s.WhenStarted(h => h.Start());
                            s.WhenStopped(h => h.Stop());
                        });
                
                x.SetDescription("Runny host to serve application's install/update/uninstall request");        
                x.SetDisplayName("Runny");                       
                x.SetServiceName("Runny");                       
            });                                                  
        }
    }
}
