using System;

using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {

    IActor _Actor;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Set(IActor actor)
    {
        _Actor = actor;
    }

    public Guid Id { get { return _Actor.Id; } }
}
