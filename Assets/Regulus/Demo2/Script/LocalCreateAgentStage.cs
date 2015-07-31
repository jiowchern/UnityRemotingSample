using System.Net;

using Regulus.Utility;

using UnityEngine;

internal class LocalCreateAgentStage : IStage
{

    public delegate void DoneCallback(string ip, int port);

    public event DoneCallback DoneEvent;
    private string _Port;

    private string _IPAddress;

    public LocalCreateAgentStage()
    {
        _Port = "4321";
        _IPAddress = "127.0.0.1";
    }

    void IStage.Enter()
    {
        
    }

    void IStage.Leave()
    {
        
    }

    void IStage.Update()
    {
        
    }

    public void DrawWindow(int id)
    {
        GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();
            GUILayout.Label("IP");
            _IPAddress = GUILayout.TextField(_IPAddress);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Port");
            _Port = GUILayout.TextField(_Port);
            GUILayout.EndHorizontal();

            

        if (GUILayout.Button("Connect"))
        {
            int port; 
            IPAddress ipaddress;
            bool fail;
            if (fail = !int.TryParse(this._Port, out port))
            {
                this._Port = "Please enter a valid port.";
            }
            if (fail = !System.Net.IPAddress.TryParse(this._IPAddress, out ipaddress))
            {
                this._IPAddress = "Please enter a valid ip.";
            }

            if (fail == false && this.DoneEvent!= null)
                this.DoneEvent(this._IPAddress, port);
            
        }
        GUILayout.EndVertical();
    }


}