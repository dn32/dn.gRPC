using ProtoBuf.Grpc;
using System.ServiceModel;

namespace send.envio.gRPC;

[ServiceContract]
public interface IServicoGrpcStreamAvancado
{
    IAsyncEnumerable<RetornoDeTeste> EnvioDuplexAsync(IAsyncEnumerable<RequisicaoDeTeste> bar, CallContext context = default);
}
