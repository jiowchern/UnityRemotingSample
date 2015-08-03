using UnityEngine;
using System.Linq;
using System.Collections;

using Regulus.Remoting;

public class World : MonoBehaviour {

    

    public IAgent Agent { get; set; }

    public GameObject ActorPrefab;

    void OnDestroy()
    {
        Agent.QueryNotifier<IActor>().Unsupply -= _ActorLeft;
        Agent.QueryNotifier<IActor>().Supply -= _ActorJoin;
    }
    // Use this for initialization
	void Start () 
    {
        Agent.QueryNotifier<IActor>().Unsupply += _ActorLeft;
        Agent.QueryNotifier<IActor>().Supply += _ActorJoin;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void _ActorLeft(IActor actor)
    {
        var obj = (from a in UnityEngine.GameObject.FindObjectsOfType<Actor>() where a.Id == actor.Id select a.gameObject).FirstOrDefault();
        if (obj != null)
            UnityEngine.Object.Destroy(obj);

    }

    private void _ActorJoin(IActor actor)
    {
        var actorObject = UnityEngine.GameObject.Instantiate(ActorPrefab);
        var actorBehaviour = actorObject.GetComponent<Actor>();
        actorBehaviour.Set(actor);
    }
}
