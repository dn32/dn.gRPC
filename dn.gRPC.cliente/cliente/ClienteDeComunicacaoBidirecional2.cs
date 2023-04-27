using send.envio.gRPC;
using System.Runtime.CompilerServices;

namespace dn.gRPC.cliente.cliente;

public class ClienteDeComunicacaoBidirecional2
{
    public IServicoGrpcStreamSimples ServicoGrpc2 { get; }

    // Aqui injetamos a dependência do serviço gRPC
    public ClienteDeComunicacaoBidirecional2(IServicoGrpcStreamSimples servicoGrpc2)
    {
        ServicoGrpc2 = servicoGrpc2;
    }

    public async IAsyncEnumerable<int> ComunicarAsync()
    {
        Console.WriteLine($"{DateTime.Now:HH:mm:ss fff}: Iniciando a comunicação com o servidor no teste 2");
        await foreach (var item in ServicoGrpc2.TestAsync(ObterValoresParaEnvioEmStream()))
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss fff}: Recebido pelo cliente: {item.Resultado}");
            yield return item.Resultado;
        }
    }

    static async IAsyncEnumerable<RequisicaoDeTeste> ObterValoresParaEnvioEmStream([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        for (int i = 0; i < 10; i++)
        {
            await Task.Delay(1000, cancellationToken);
            var next = new RequisicaoDeTeste { X = i, Y = i };
            yield return next;
            Console.WriteLine($"{DateTime.Now:HH:mm:ss fff}: Enviando do cliente: {next.X}, {next.Y}");
        }

        Console.WriteLine($"{DateTime.Now:HH:mm:ss fff}: Envio concluído");
    }
}