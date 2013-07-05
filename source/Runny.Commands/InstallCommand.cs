using Agatha.Common;
using System;
namespace Runny.Commands
{
    public class InstallRequest : Request
    {
        public String ApplicationName { get; set; }
        public String Location { get; set; }
        public String NugetSource { get; set; }
    }

    public class InstallResponse : Response
    {
    }
}
