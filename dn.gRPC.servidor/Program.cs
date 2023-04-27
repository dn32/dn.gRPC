using Microsoft.AspNetCore.Builder;

namespace dn.gRPC.servidor;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Iniciando o servidor");

        var builder = WebApplication.CreateBuilder(args);

        // Adicionamos o servidor gRPC
        builder.Services.AddServidorDeGrpc();
        // Injetamos a dependência do servidor gRPC aqui
        builder.Services.AddScoped<ServicoGrpcStreamAvancado, ServicoGrpcStreamAvancado>();
        builder.Services.AddScoped<ServicoGrpcStreamSimples, ServicoGrpcStreamSimples>();

        var app = builder.Build();
        app.UseRouting();

        // Mapeamos o controlador gRPC
        app.MapGrpcService<ServicoGrpcStreamAvancado>();
        app.MapGrpcService<ServicoGrpcStreamSimples>();

        app.Run();
    }
}