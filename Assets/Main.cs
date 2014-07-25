using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour, Regulus.Utility.Console.IInput, Regulus.Utility.Console.IViewer
{
    Sample.Client _Client;	
    string _Temp;

    System.Collections.Generic.Queue<string> _Messages;
	void Start () 
    {
        
        _InputText = "";
        _Temp = "";
        _Messages = new System.Collections.Generic.Queue<string>();

        _Client = new Sample.Client(this, this);
        _Client.Launch();        
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (_Client.Process() == false)
        {
            Application.Quit();
        }
	}

    void OnDestroy()
    {
        _Client.Shutdown();
    }

    void OnGUI()
    {
        GUILayout.BeginVertical();
        foreach(var message in _Messages)
        {
            GUILayout.Label(message);
        }
        GUILayout.EndVertical();

        GUILayout.BeginHorizontal();
        _InputText = GUILayout.TextField(_InputText);
        if(GUILayout.Button("Send"))
        {
            _Send(_InputText);
            _InputText = "";
        }
        GUILayout.EndHorizontal();
    }

    private void _Send(string input_text)
    {
        var command = input_text.Split(new string[]{" "} , System.StringSplitOptions.RemoveEmptyEntries );
        _OutputEvent(command);
    }
    event Regulus.Utility.Console.OnOutput _OutputEvent;
    private string _InputText;
    event Regulus.Utility.Console.OnOutput Regulus.Utility.Console.IInput.OutputEvent
    {
        add { _OutputEvent += value; }
        remove { _OutputEvent -= value; }
    }

    
    void Regulus.Utility.Console.IViewer.Write(string message)
    {     
        _Temp += message;
        if(message == "\n")
        {
            _Add();                    
        }
    }

    private void _Add()
    {
        _Messages.Enqueue(_Temp);
        _Temp = "";
        if (_Messages.Count > 20)
            _Messages.Dequeue();
    }

    void Regulus.Utility.Console.IViewer.WriteLine(string message)
    {        
        _Temp += message;
        _Add();
    }
}
