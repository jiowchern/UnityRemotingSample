﻿using UnityEngine;
using System.Collections;

public class Sample : MonoBehaviour {

    Regulus.Remoting.IAgent _Agent;
	// Use this for initialization
	void Start () 
    {
        _Agent = Regulus.Remoting.Ghost.Native.Agent.Create();
        _Agent.Launch();

        Debug.Log("begin connect...");
        _Agent.Connect("127.0.0.1", 12345).OnValue += _ConnectResult;
        _Agent.QueryNotifier<Custom.ISample>().Supply += _GetSample;
	}
    void _GetSample(Custom.ISample sample)
    {
        Debug.Log("get Sample...");
        sample.Add(1, 2).OnValue += (result) => { Debug.Log(string.Format("Add(1 , 2) == {0}", result)); };
        sample.GetSubtractor().OnValue += _GetSubtractor;
    }

    private void _GetSubtractor(Custom.ISubtractor subtractor)
    {
        Debug.Log("get Subtractor...");
        subtractor.Sub(1, 2).OnValue += (result) => { Debug.Log(string.Format("Sub(1 , 2) == {0}", result)); };
    }

    private void _ConnectResult(bool success)
    {
        if (success)
        {
            Debug.Log("Connect success.");
        }
        else
            Debug.Log("Connect fail.");
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