using ProtoBuf.Grpc;
using send.envio.gRPC;

namespace dn.gRPC.servidor;

public class ServicoGrpcStreamSimples : IServicoGrpcStreamSimples
{
    public async IAsyncEnumerable<RetornoDeTeste> TestAsync(IAsyncEnumerable<RequisicaoDeTeste> requisicao, CallContext context)
    {
        Console.WriteLine($"{DateTime.Now:HH:mm:ss fff}: Iniciando a entrega de dados do servidor.");

        await foreach (var item in requisicao.AsChannelReader().ReadAllAsync())
        {
            yield return new RetornoDeTeste { Resultado = item.X * item.Y };
        }
    }
}
