using ProtoBuf.Grpc;
using send.envio.gRPC;

namespace dn.gRPC.servidor;

public class ServicoGrpcStreamAvancado : IServicoGrpcStreamAvancado
{
    public IAsyncEnumerable<RetornoDeTeste> EnvioDuplexAsync(IAsyncEnumerable<RequisicaoDeTeste> bar, CallContext context)
    {
        return context.FullDuplexAsync(ProduceAsync, bar, ConsumeAsync);
    }

    private async IAsyncEnumerable<RetornoDeTeste> ProduceAsync(CallContext context)
    {
        for (int i = 0; i < 4; i++)
        {
            await Task.Delay(TimeSpan.FromSeconds(2.5), context.CancellationToken);

            var result = new RetornoDeTeste { Resultado = i };
            yield return result;
            Console.WriteLine($"{DateTime.Now:HH:mm:ss fff}: Repondendo para o cliente: {result.Resultado}");
        }

        Console.WriteLine($"{DateTime.Now:HH:mm:ss fff}: Finalizadas as respostas");
    }

    private ValueTask ConsumeAsync(RequisicaoDeTeste item, CallContext context)
    {
        Console.WriteLine($"{DateTime.Now:HH:mm:ss fff}: Recebido do cliente {item.X}, {item.Y} from {context.ServerCallContext?.Peer}");
        return default;
    }
}
