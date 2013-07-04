using Agatha.Common;
using Agatha.ServiceLayer;
using Runny.Commands;

namespace Runny.Handlers
{
    class EchoHandler : RequestHandler<EchoRequest, EchoResponse>
    {
        public override Response Handle(EchoRequest request)
        {
            var response = CreateTypedResponse();
            response.Message = request.Message;

            return response;
        }
    }
}
