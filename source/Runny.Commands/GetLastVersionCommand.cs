using Agatha.Common;
using System;

namespace Runny.Commands
{
    public class GetLastVersionRequest : Request
    {
        public String ApplicationName { get; set; }
        public String NugetSource { get; set; }
    }

    public class GetLastVersionResponse : Response
    {
        public String Version { get; set; }
    }
}
