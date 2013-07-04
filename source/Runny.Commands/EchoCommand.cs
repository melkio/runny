using Agatha.Common;
using System;

namespace Runny.Commands
{
    public class EchoRequest : Request
    {
        public String Message { get; set; }
    }

    public class EchoResponse : Response
    {
        public String Message { get; set; }
    }
}
