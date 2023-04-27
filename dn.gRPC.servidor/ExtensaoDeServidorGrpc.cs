using Microsoft.Extensions.DependencyInjection.Extensions;
using ProtoBuf.Grpc.Configuration;
using ProtoBuf.Grpc.Server;

namespace dn.gRPC.servidor;

public static class ExtensaoDeServidorGrpc
{
    public static IServiceCollection AddServidorDeGrpc(this IServiceCollection services)
    {
        services.AddGrpc();
        services.AddCodeFirstGrpc(config =>
        {
            config.EnableDetailedErrors = true;
            config.MaxReceiveMessageSize = int.MaxValue;
            config.MaxSendMessageSize = int.MaxValue;
            config.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal;
        });

        services.TryAddSingleton(BinderConfiguration.Create(binder: new ServicoDeObtensaoDeDependencias(services)));
        services.AddCodeFirstGrpcReflection();

        return services;
    }
}