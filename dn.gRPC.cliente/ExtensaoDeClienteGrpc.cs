using Grpc.Core;
using Grpc.Net.Client.Configuration;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;

namespace dn.gRPC.cliente;

public static class ExtensaoDeClienteGrpc
{
    public static IServiceCollection AddServicoGrpcAsync<TServico>(this IServiceCollection services, ServiceLifetime serviceLifetime, ConfigurationManager configuration, string endereco) where TServico : class
    {
        var servico = ConectarGrpcAsync<TServico>(configuration, endereco);
        var descricao = ServiceDescriptor.Describe(typeof(TServico), (serviceProvider) => servico, serviceLifetime);

        if (!services.Contains(descricao))
        {
            services.Add(descricao);
        }

        return services;
    }

    public static TServico ConectarGrpcAsync<TServico>(ConfigurationManager configuration, string endereco) where TServico : class
    {
        var http = CriarCanalGRPC(endereco);
        return http.CreateGrpcService<TServico>();
    }

    public static GrpcChannel CriarCanalGRPC(string url)
    {
        var defaultMethodConfig = new MethodConfig
        {
            Names = { MethodName.Default },
            RetryPolicy = new RetryPolicy
            {
                MaxAttempts = 5,
                InitialBackoff = TimeSpan.FromSeconds(1),
                MaxBackoff = TimeSpan.FromSeconds(5),
                BackoffMultiplier = 1.5,
                RetryableStatusCodes = { StatusCode.Unavailable }
            }
        };

        {
            var handler = new SocketsHttpHandler
            {
                PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
                EnableMultipleHttp2Connections = true,
                ConnectTimeout = TimeSpan.FromSeconds(5),
            };

            return GrpcChannel.ForAddress(url, new GrpcChannelOptions
            {
                HttpHandler = handler,
                Credentials = ChannelCredentials.Insecure,
                ServiceConfig = new ServiceConfig { MethodConfigs = { defaultMethodConfig } },
                MaxReceiveMessageSize = int.MaxValue,
                MaxSendMessageSize = int.MaxValue
            });
        }
    }
}