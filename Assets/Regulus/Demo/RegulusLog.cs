using UnityEngine;
using System.Collections;

public class RegulusLog : MonoBehaviour {

	// Use this for initialization

    void Awake()
    {
        //Regulus.Utility.Log.Instance.RecordEvent += Instance_RecordEvent;
    }
	void Start () {
        
	}

    void Instance_RecordEvent(string message)
    {
        Debug.Log(message);
    }

    void OnDestroy()
    {
        Regulus.Utility.Log.Instance.RecordEvent -= Instance_RecordEvent;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
