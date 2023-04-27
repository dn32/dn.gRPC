using ProtoBuf.Grpc;
using send.envio.gRPC;
using System.Threading.Channels;

namespace dn.gRPC.cliente.cliente;

public class ClienteDeComunicacaoBidirecional1
{
    public IServicoGrpcStreamAvancado ControladorDeEntradaDeRequisicoesGrpc { get; }

    // Aqui injetamos a dependência do serviço gRPC
    public ClienteDeComunicacaoBidirecional1(IServicoGrpcStreamAvancado controladorDeEntradaDeRequisicoesGrpc)
    {
        ControladorDeEntradaDeRequisicoesGrpc = controladorDeEntradaDeRequisicoesGrpc;
    }

    public async ValueTask ComunicarAsync()
    {
        Console.WriteLine($"{DateTime.Now:HH:mm:ss fff}: Iniciando a comunicação com o servidor");
        var canal = Channel.CreateBounded<RequisicaoDeTeste>(5);

        var contexto = new CallContext(state: canal.Writer);
        var valor = canal.AsAsyncEnumerable(contexto.CancellationToken);
        var result = ControladorDeEntradaDeRequisicoesGrpc.EnvioDuplexAsync(valor);
        await contexto.FullDuplexAsync(EnviarInformacaoAsync, result, ReceberInformacaoAsync);
    }

    private async ValueTask EnviarInformacaoAsync(CallContext ctx)
    {
        var writer = ctx.As<ChannelWriter<RequisicaoDeTeste>>();
        try
        {
            for (int i = 0; i < 5; i++)
            {
                var item = new RequisicaoDeTeste { X = 40 + i, Y = 40 + i };
                await writer.WriteAsync(item, ctx.CancellationToken);
                Console.WriteLine($"{DateTime.Now:HH:mm:ss fff}: Enviando do cliente: {item.X}, {item.Y}");
                await Task.Delay(TimeSpan.FromSeconds(0.5));
            }
            writer.Complete();
            Console.WriteLine($"{DateTime.Now:HH:mm:ss fff}: Envio concluído");
        }
        catch (Exception ex) { writer.TryComplete(ex); }
    }

    private ValueTask ReceberInformacaoAsync(RetornoDeTeste result, CallContext arg2)
    {
        Console.WriteLine($"{DateTime.Now:HH:mm:ss fff}: Recebido pelo cliente: {result.Resultado}");
        return default;
    }
}