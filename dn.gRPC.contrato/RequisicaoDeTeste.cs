using System.Runtime.Serialization;

namespace send.envio.gRPC;

[DataContract]
public class RequisicaoDeTeste
{
    [DataMember(Order = 1)]
    public int X { get; set; }

    [DataMember(Order = 2)]
    public int Y { get; set; }
}
