using System;

public interface IActor
{
    Guid Id { get; }

    int Color { get; }

    event Action<float ,float ,float> MoveEvent;

    float Speed { get; }
}