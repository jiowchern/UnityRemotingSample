
[ProtoBuf.ProtoContract]
public class MoveData
{
    [ProtoBuf.ProtoMember(1)]
    public float VectorX;
    [ProtoBuf.ProtoMember(2)]
    public float VectorY;

    [ProtoBuf.ProtoMember(3)]
    public float FirstX;
    [ProtoBuf.ProtoMember(4)]
    public float FirstY;
    [ProtoBuf.ProtoMember(5)]
    public float Speed;

}