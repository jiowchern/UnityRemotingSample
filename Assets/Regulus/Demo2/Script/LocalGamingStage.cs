using Regulus.Remoting;
using Regulus.Utility;

using UnityEngine;
using System.Linq;

internal class LocalGamingStage : IStage
{
    private UnityEngine.GameObject _ActorPrefab;

    public delegate void DoneCallback();

    public event DoneCallback DoneEvent;
    private readonly IAgent _Agent;

    public LocalGamingStage(IAgent agent)
    {
        _Agent = agent;
        
    }

    void IStage.Enter()
    {
        _Agent.BreakEvent += _Done;


        _Agent.QueryNotifier<IActor>().Supply += _ActorJoin;
        _Agent.QueryNotifier<IActor>().Unsupply += _ActorLeft;
    }

    private void _ActorLeft(IActor actor)
    {
        var obj = (from a in UnityEngine.GameObject.FindObjectsOfType<Actor>() where a.Id == actor.Id select a.gameObject).FirstOrDefault();
        if (obj != null)
            UnityEngine.Object.Destroy(obj);
        
    }

    private void _ActorJoin(IActor actor)
    {
        var actorObject = UnityEngine.GameObject.Instantiate(_ActorPrefab);
        var actorBeh = actorObject.GetComponent<Actor>();
        actorBeh.Set(actor);
    }

    private void _Done()
    {
        DoneEvent();
    }

    void IStage.Leave()
    {
        _Agent.BreakEvent -= this._Done;
        _Agent.Shutdown();
    }

    void IStage.Update()
    {
        

        _Agent.Update();
    }

    public void DrawWindow(int id)
    {
        if (GUILayout.Button("Quit"))
        {
            _Done();
        }
    }
}