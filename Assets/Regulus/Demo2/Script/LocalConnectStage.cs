using System.Collections.Generic;

using Regulus.Remoting;
using Regulus.Utility;

using UnityEngine;

internal class LocalConnectStage : IStage
{

    public delegate void DoneCallabck(Regulus.Remoting.IAgent agent);
    public delegate void FailCallabck();

    public event DoneCallabck DoneEvent;

    public event FailCallabck FailEvent;
    private readonly string _Ip;

    private readonly int _Port;

    private List<string> _Messages;

    private bool? _Result;

    private IAgent _Agent;

    public LocalConnectStage(string ip, int port)
    {
        this._Ip = ip;
        this._Port = port;

        _Messages = new List<string>();
    }

    void IStage.Enter()
    {
        var agent = Regulus.Remoting.Ghost.Native.Agent.Create();
        agent.Launch();
        _Messages.Add(string.Format("Start connecton to {0}:{1}..." , _Ip , _Port));
        agent.Connect(_Ip, _Port).OnValue += _ConnectResult;

        _Agent = agent;
    }

    private void _ConnectResult(bool success)
    {
        if (success)
            _Messages.Add(string.Format("Connection success."));
        else
            _Messages.Add(string.Format("Connection failed."));

        _Result = success;
    }

    void IStage.Leave()
    {
        
    }

    void IStage.Update()
    {
        _Agent.Update();
    }

    public void DrawWindow(int id)
    {
        
        GUILayout.BeginVertical();

        foreach (var message in _Messages)
        {
            GUILayout.Label(message);
        }
        
        GUILayout.EndVertical();


        if (_Result.HasValue)
        {
            if (GUILayout.Button("Next"))
            {
                if (_Result.Value)
                {
                    DoneEvent(_Agent);
                }
                else
                {
                    FailEvent();
                }    
            }
            
        }
    }
}