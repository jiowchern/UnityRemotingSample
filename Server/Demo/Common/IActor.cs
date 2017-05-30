using System;

namespace Regulus.Project.RemoteDemo.Common
{

    public interface IActor
    {
        Guid Id { get; }

        float ColorR { get; }

        float ColorG { get; }

        float ColorB { get; }

        event Action<MoveData> MoveEvent;


    }
}