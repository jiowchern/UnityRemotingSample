using UnityEngine;
using System.Collections;

using Regulus.Remoting;

public class Local : MonoBehaviour {

    Regulus.Utility.StageMachine _Machine;
    public Rect WindowRect;
    GUI.WindowFunction _DrawWindow;

    // Use this for initialization
    void Start()
    {
        _Machine = new Regulus.Utility.StageMachine();

        _InitialWindow();
        this._ToSetting();
    }

    private void _InitialWindow()
    {
        _DrawWindow = Local._EmptyDraw;
    }

    private static void _EmptyDraw(int id)
    {

    }


    private void _ToSetting()
    {
        var stage = new LocalCreateAgentStage();
        stage.DoneEvent += _ToConnect;
        _DrawWindow = stage.DrawWindow;
        _Machine.Push(stage);
    }

    private void _ToConnect(string ip, int port)
    {
        var stage = new LocalConnectStage(ip , port);
        stage.DoneEvent += this._ToGaming;
        stage.FailEvent += _ToSetting;
        _DrawWindow = stage.DrawWindow;
        _Machine.Push(stage);
    }

    private void _ToGaming(IAgent agent)
    {
        var stage = new LocalGamingStage(agent);
        stage.DoneEvent += _ToSetting;        
        _DrawWindow = stage.DrawWindow;
        _Machine.Push(stage);
    }

    void Update()
    {
        _Machine.Update();
    }


    void OnGUI()
    {
        this.WindowRect = GUILayout.Window(1, WindowRect, _DrawWindow, "Local");
    }
}