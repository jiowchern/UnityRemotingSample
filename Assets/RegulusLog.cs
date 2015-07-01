using UnityEngine;
using System.Collections;

public class RegulusLog : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Regulus.Utility.Log.Instance.RecordEvent += Instance_RecordEvent;
	}

    void Instance_RecordEvent(string message)
    {
        Debug.Log(message);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
