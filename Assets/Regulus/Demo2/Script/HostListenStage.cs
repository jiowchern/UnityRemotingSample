using Regulus.Utility;

using UnityEngine;

internal class HostListenStage : IStage
{
    private readonly Regulus.Remoting.Soul.Native.Server _Server;

    public delegate void DoneCallabck();
    public event DoneCallabck DoneEvent;

    public HostListenStage(Regulus.Remoting.Soul.Native.Server server)
    {
        this._Server = server;
    }

    void IStage.Enter()
    {
        _Server.Launch();
    }

    void IStage.Leave()
    {
        _Server.Shutdown();
    }

    void IStage.Update()
    {        
    }

    public void DrawWindow(int id)
    {
        GUILayout.BeginVertical();


        GUILayout.Label(string.Format("Number of connections : {0}", _Server.PeerCount));

        GUILayout.Label(string.Format("Core Usage : {0:P}", _Server.CoreUsage));
        GUILayout.Label(string.Format("Peer Usage : {0:P}", _Server.PeerUsage));
        
        GUILayout.Label(string.Format("Core PFS : {0}", _Server.CoreFPS));
        GUILayout.Label(string.Format("Peer PFS : {0}", _Server.PeerFPS));

        GUILayout.Label(string.Format("Read bytes per second : {0}", _Server.ReadBytesPerSecond));
        GUILayout.Label(string.Format("Write bytes per second : {0}", _Server.WriteBytesPerSecond));


        if (GUILayout.Button("End"))
        {
            DoneEvent();
        }

        GUILayout.EndVertical();
    }
}