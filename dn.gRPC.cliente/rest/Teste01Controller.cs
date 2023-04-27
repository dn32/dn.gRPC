using dn.gRPC.cliente.cliente;
using Microsoft.AspNetCore.Mvc;

namespace dn.gRPC.cliente.rest;

[ApiController]
[Route("[Controller]")]
public class Teste01Controller : Controller
{
    public Teste01Controller(ClienteDeComunicacaoBidirecional1 clienteDeComunicacaoBidirecional1)
    {
        ClienteDeComunicacaoBidirecional1 = clienteDeComunicacaoBidirecional1;
    }

    public ClienteDeComunicacaoBidirecional1 ClienteDeComunicacaoBidirecional1 { get; }

    [HttpGet]
    public ValueTask TesteAsync()
    {
        return ClienteDeComunicacaoBidirecional1.ComunicarAsync();
    }
}