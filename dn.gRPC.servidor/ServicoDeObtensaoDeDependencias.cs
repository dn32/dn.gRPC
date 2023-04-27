using ProtoBuf.Grpc.Configuration;

namespace dn.gRPC.servidor;

internal class ServicoDeObtensaoDeDependencias : ServiceBinder
{
    private readonly IServiceCollection services;

    public ServicoDeObtensaoDeDependencias(IServiceCollection services)
    {
        this.services = services;
    }
}
