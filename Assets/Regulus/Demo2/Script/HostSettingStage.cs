using System.Collections.Generic;
using System.Linq;
using System.Text;

using Regulus.Utility;

using UnityEngine;



public class HostSettingStage : IStage
{
    private string _Port;

    public delegate void DoneCallabck(Regulus.Remoting.Soul.Native.Server server);

    public event DoneCallabck DoneEvent;

    public HostSettingStage()
    {
        _Port = "4321";
    }

    void Regulus.Utility.IStage.Enter()
    {
        
    }

    void Regulus.Utility.IStage.Leave()
    {
        
    }

    void Regulus.Utility.IStage.Update()
    {
        
    }

    public void DrawWindow(int id)
    {
        GUILayout.BeginVertical();

            

            GUILayout.BeginHorizontal();
            GUILayout.Label("Port");
            _Port = GUILayout.TextField(_Port);
            GUILayout.EndHorizontal();


        if (GUILayout.Button("Listen"))
        {
            int port;
            if (int.TryParse(_Port, out port))
            {
                _CreateServer(port);    
            }
            else
            {
                _Port = "Please enter a valid port.";
            }
        }
        GUILayout.EndVertical();
        
    }

    private void _CreateServer(int port)
    {
        var server = new Regulus.Remoting.Soul.Native.Server(new HostCore() , port);
        DoneEvent(server);
    }
}