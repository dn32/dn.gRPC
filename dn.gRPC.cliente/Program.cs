using dn.gRPC.cliente.cliente;
using Microsoft.AspNetCore.Builder;
using send.envio.gRPC;

namespace dn.gRPC.cliente;


internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($"Iniciando o client");

        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;

        // Aqui adicionamos a referência do serviço gRPC ao client
        services.AddServicoGrpcAsync<IServicoGrpcStreamAvancado>(ServiceLifetime.Scoped, builder.Configuration, "http://localhost:5093");
        services.AddServicoGrpcAsync<IServicoGrpcStreamSimples>(ServiceLifetime.Scoped, builder.Configuration, "http://localhost:5093");
        services.AddControllers();
        services.AddScoped<ClienteDeComunicacaoBidirecional1>();
        services.AddScoped<ClienteDeComunicacaoBidirecional2>();
        var app = builder.Build();
        app.MapControllers();
        app.UseRouting();
        app.Run();
    }
}