using ProtoBuf.Grpc;
using System.ServiceModel;

namespace send.envio.gRPC;

[ServiceContract]
public interface IServicoGrpcStreamSimples
{
    IAsyncEnumerable<RetornoDeTeste> TestAsync(IAsyncEnumerable<RequisicaoDeTeste> request, CallContext options = default);
}