using dn.gRPC.cliente.cliente;
using Microsoft.AspNetCore.Mvc;

namespace dn.gRPC.cliente.rest;

[ApiController]
[Route("[Controller]")]
public class Teste02Controller : Controller
{
    private ClienteDeComunicacaoBidirecional2 ClienteDeComunicacaoBidirecional2 { get; }

    public Teste02Controller(ClienteDeComunicacaoBidirecional2 clienteDeComunicacaoBidirecional2)
    {
        ClienteDeComunicacaoBidirecional2 = clienteDeComunicacaoBidirecional2;
    }

    [HttpGet]
    public IAsyncEnumerable<int> TesteAsync()
    {
        return ClienteDeComunicacaoBidirecional2.ComunicarAsync();
    }
}
