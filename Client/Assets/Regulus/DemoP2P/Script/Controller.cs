using Regulus.Remoting;

using UnityEngine;
using Regulus.Project.RemoteDemo.Common;
internal class Controller : MonoBehaviour
{
    private IController _Controller;

    public Rect WindowRect;

    private int _CurrentSelect;

    private int _Previous;

    private string _TextR;

    private string _TextG;

    private string _TextB;

    public IAgent Agent { get; set; }


    void OnDestroy()
    {
        Agent.QueryNotifier<IController>().Supply -= _SupplyController;
    }
    void Start()
    {
        _TextR = "0";
        _TextG = "0";
        _TextB = "0";
        Agent.QueryNotifier<IController>().Supply += _SupplyController;
    }

    private void _SupplyController(IController obj)
    {
        _Controller = obj;
    }


    void OnGUI()
    {
        if (_Controller != null)
        {
            this.WindowRect = GUILayout.Window(2, WindowRect, _DrawWindow, "Controller");        
        }
    }



    private void _DrawWindow(int id)
    {
        GUILayout.BeginVertical();

        _CurrentSelect = GUILayout.SelectionGrid(_CurrentSelect, new string[] {"left top" , "top" , "right top" , "left" , "stop" , "right" , "left bottom" , "bottom" , "right bottom"}, 3);

        if (_Previous != _CurrentSelect)
        {
            _Previous = _CurrentSelect;
            _Move(_CurrentSelect);
        }
        
        GUILayout.BeginHorizontal();
        _TextR = GUILayout.TextField(_TextR);
        _TextG = GUILayout.TextField(_TextG);
        _TextB = GUILayout.TextField(_TextB);

        if (GUILayout.Button("Change Color"))
        {
            float r,
                  g,
                  b;

            if (float.TryParse(_TextR, out r) && float.TryParse(_TextG, out g) && float.TryParse(_TextB, out b))
            {
                _Controller.SetColor(r,g,b);
            }
        }
        
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
    }

    private void _Move(int index)
    {
        Vector2[] dir = new Vector2[]
                            {
                                new Vector2(-1, 1), new Vector2(0, 1), new Vector2(1, 1),
                                new Vector2(-1, 0), new Vector2(0, 0), new Vector2(1, 0),
                                new Vector2(-1, -1), new Vector2(0, -1), new Vector2(1, -1)
                            };


        _Controller.Move(dir[index].x, dir[index].y);
    }
}