using UnityEngine;
using System.Collections;

using Regulus.Utility;

public class Server : MonoBehaviour ,
    Regulus.Utility.Console.IViewer 
{

    Custom.Appliction _Application;
    Regulus.Remoting.Soul.Native.Server _Server;

    private ICommand _Command;

    // Use this for initialization
    void Start ()
    {
        _Command = new Command();

        
        _Application = new Custom.Appliction(this);
        var protocol = new Regulus.Project.RemoteDemo.Protocol();
        _Server = new Regulus.Remoting.Soul.Native.Server(_Application, protocol , _Command, 12345);
        _Server.Launch();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        _Server.Shutdown();
    }

    void Regulus.Utility.Console.IViewer.Write(string message)
    {
        Debug.Log(message);
    }

    void Regulus.Utility.Console.IViewer.WriteLine(string message)
    {
        Debug.Log(message);
    }


}
