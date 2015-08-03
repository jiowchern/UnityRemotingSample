using System;

using UnityEngine;
using System.Collections;



public class Actor : MonoBehaviour {

    IActor _Actor;

    public float _Speed;

    private Vector2 _Direction;

    public Renderer Renderer;

    void OnDestroy()
    {
        _Actor.MoveEvent -= _Move;
    }
	// Use this for initialization
	void Start ()
	{
	    
	    _Actor.MoveEvent += _Move;
	}

    private void _Move(MoveData data)
    {
        _Speed = data.Speed;
        _Direction = new Vector2(data.VectorX, data.VectorY);

        var pos = new Vector2(data.FirstX , data.FirstY);
        gameObject.transform.position = new Vector3(pos.x , 0 ,pos.y);
    }

    // Update is called once per frame
	void Update () 
    {
	    if (_Speed != 0)
	    {
	        var dir = _Direction;
	        var current = gameObject.transform.position;
	        var offset = dir * _Speed * UnityEngine.Time.deltaTime;

	        gameObject.transform.position = current + new Vector3(offset.x ,0 , offset.y);
	    }

        this.Renderer.material.color = new Color(_Actor.ColorR, _Actor.ColorG, _Actor.ColorB);         
        
    }

    public void Set(IActor actor)
    {
        _Actor = actor;
    }

    public Guid Id { get { return _Actor.Id; } }
}
