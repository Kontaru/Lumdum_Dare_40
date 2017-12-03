using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Boss_Movement : MonoBehaviour {

    private float nextTurnTime;
    private Transform startTransform;

    public float multiplyBy;

    NavMeshAgent enemy;
    public GameObject Target;
    public bool BL_Alerted = false;

	// Use this for initialization
	void Start () {
        enemy = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(Target.transform.position, transform.position) < 3.0f)
            BL_Alerted = true;

        if(BL_Alerted)
        {
            if (Vector3.Distance(Target.transform.position, transform.position) < 5.0f)
                RunFrom();
            else if (Vector3.Distance(Target.transform.position, transform.position) >= 5.0f)
                ChaseTo();
        }
	}

    public void ChaseTo()
    {
        enemy.destination = Target.transform.position;
    }

    public void RunFrom()
    {
        // store the starting transform
        startTransform = transform;

        //temporarily point the object to look away from the player
        transform.rotation = Quaternion.LookRotation(transform.position - Target.transform.position);

        //Then we'll get the position on that rotation that's multiplyBy down the path (you could set a Random.range
        // for this if you want variable results) and store it in a new Vector3 called runTo
        Vector3 runTo = transform.position + transform.forward * multiplyBy;
        //Debug.Log("runTo = " + runTo);

        //So now we've got a Vector3 to run to and we can transfer that to a location on the NavMesh with samplePosition.

        NavMeshHit hit;    // stores the output in a variable called hit

        // 5 is the distance to check, assumes you use default for the NavMesh Layer name
        NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetNavMeshLayerFromName("Default"));
        //Debug.Log("hit = " + hit + " hit.position = " + hit.position);

        // just used for testing - safe to ignore
        nextTurnTime = Time.time + 5;

        // reset the transform back to our start transform
        transform.position = startTransform.position;
        transform.rotation = startTransform.rotation;

        // And get it to head towards the found NavMesh position
        enemy.SetDestination(hit.position);
    }
}
