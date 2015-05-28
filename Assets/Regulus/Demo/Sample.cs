using UnityEngine;
using System.Collections;

public class Sample : MonoBehaviour {

    Regulus.Remoting.IAgent _Agent;
	// Use this for initialization
	void Start () 
    {
        _Agent = Regulus.Remoting.Ghost.Native.Agent.Create();
        _Agent.Launch();

        Debug.Log("開始連線...");
        _Agent.Connect("127.0.0.1", 12345).OnValue += _ConnectResult;
        _Agent.QueryNotifier<Custom.ISample>().Supply += _GetSample;
	}
    void _GetSample(Custom.ISample sample)
    {
        Debug.Log("取得Sample...");
        sample.Add(1, 2).OnValue += (result) => { Debug.Log(string.Format("Add(1 , 2) == {0}", result)); };
        sample.GetSubtractor().OnValue += _GetSubtractor;
  

        Debug.Log("輸入GetSeconds可以取得伺服器端物件的資料");        

    }

    private void _GetSubtractor(Custom.ISubtractor subtractor)
    {
        Debug.Log("取得Subtractor...");
        subtractor.Sub(1, 2).OnValue += (result) => { Debug.Log(string.Format("Sub(1 , 2) == {0}", result)); };
    }

    private void _ConnectResult(bool success)
    {
        if (success)
        {
            Debug.Log("連線成功.");
        }
        else
            Debug.Log("連線失敗.");
    }

	// Update is called once per frame
	void Update () {
        _Agent.Update();
	}

    void OnDestroy()
    {
        _Agent.Shutdown();
    }
}
