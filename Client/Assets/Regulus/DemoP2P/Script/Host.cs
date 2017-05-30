using UnityEngine;
using System.Collections;

public class Host : MonoBehaviour {

    Regulus.Utility.StageMachine _Machine;
    public Rect WindowRect;
    GUI.WindowFunction _DrawWindow;

	// Use this for initialization
	void Start () 
    {
        _Machine = new Regulus.Utility.StageMachine();

        _InitialWindow();
        this._ToSetting();
	}

    private void _InitialWindow()
    {
        _DrawWindow = _EmptyDraw;        
    }

    private void _EmptyDraw(int id)
    {
        
    }

    private void _ToSetting()
    {
        var stage = new HostSettingStage();
        stage.DoneEvent += _ToListen;
        _DrawWindow = stage.DrawWindow;
        _Machine.Push(stage);
    }

    private void _ToListen(Regulus.Remoting.Soul.Native.Server server)
    {
        var stage = new HostListenStage(server);
        stage.DoneEvent += _ToSetting;
        _DrawWindow = stage.DrawWindow;
        _Machine.Push(stage);
    }

    // Update is called once per frame
	void Update () 
    {
        _Machine.Update();
	}


    void OnGUI()
    {
        WindowRect = GUILayout.Window(0, WindowRect, _DrawWindow, "Host");
    }
    
}