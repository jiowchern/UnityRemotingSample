using UnityEngine;
using System.Collections;

public class Server : MonoBehaviour ,
    Regulus.Utility.Console.IViewer
{

    Custom.Appliction _Application;
    Regulus.Remoting.Soul.Native.Server _Server;
    
    // Use this for initialization
    void Start () {
        _Application = new Custom.Appliction(this);
        _Server = new Regulus.Remoting.Soul.Native.Server(_Application, 12345);
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
