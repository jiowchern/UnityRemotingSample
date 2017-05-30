using Regulus.Remoting;
using Regulus.Utility;

using UnityEngine;


internal class LocalGamingStage : IStage
{

    private UnityEngine.GameObject _WorldPrefab;
    private UnityEngine.GameObject _ControllerPrefab;
    public delegate void DoneCallback();

    public event DoneCallback DoneEvent;
    private readonly IAgent _Agent;

    private World _World;

    private Controller _Controller;

    public LocalGamingStage(IAgent agent , UnityEngine.GameObject world_prefab , UnityEngine.GameObject controller_prefab)
    {
        _WorldPrefab = world_prefab;
        _Agent = agent;
        _ControllerPrefab = controller_prefab;
    }

    void IStage.Enter()
    {
        _Agent.BreakEvent += _Done;


        _CreateWorld();
        _CreateContrller();
    }

    private void _CreateWorld()
    {
        var worldObject = UnityEngine.Object.Instantiate(_WorldPrefab);
        var world = worldObject.GetComponent<World>();
        world.Agent = _Agent;

        _World = world;
    }

    private void _CreateContrller()
    {
        var controllerObject = UnityEngine.Object.Instantiate(_ControllerPrefab);
        var controller = controllerObject.GetComponent<Controller>();
        controller.Agent = _Agent;

        _Controller = controller;
    }

    void IStage.Leave()
    {
        _DestroyContrller();
        _DestroyWorld();
        _Agent.BreakEvent -= this._Done;
        _Agent.Shutdown();
    }

    private void _DestroyContrller()
    {
        
        if(_Controller.gameObject != null)
            Object.Destroy(_Controller.gameObject);
    }

    private void _DestroyWorld()
    {
        
        GameObject.Destroy(_World.gameObject);
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

    private void _Done()
    {
        DoneEvent();
    }

    

    
}