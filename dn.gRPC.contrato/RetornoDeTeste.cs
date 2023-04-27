using System.Runtime.Serialization;

namespace send.envio.gRPC;

[DataContract]
public class RetornoDeTeste
{
    [DataMember(Order = 1)]
    public int Resultado { get; set; }
}
