using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Boss_Movement : MonoBehaviour {

    //Dashes
    bool BL_Dash = false;
    public float FL_moveSpeed;
    float FL_defaultSpeed;

    float FL_Cooldown;
    public float FL_Delay = 5.0f;

    //Backing up
    private float nextTurnTime;
    private Transform startTransform;

    public float multiplyBy;

    //Important variables
    NavMeshAgent enemy;
    Rigidbody RB_Enemy;
    Vector3 direction;
    Vector3 moveInput;

    public GameObject Target;
    public bool BL_Alerted = false;

	// Use this for initialization
	void Start () {
        enemy = GetComponent<NavMeshAgent>();
        RB_Enemy = GetComponent<Rigidbody>();
        FL_Cooldown = Time.time;
        FL_defaultSpeed = FL_moveSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(Target.transform.position, transform.position) < 3.0f)
            BL_Alerted = true;

        if(BL_Alerted)
        {
            if (BL_Dash == false && Time.time > FL_Cooldown)
            {
                FL_Cooldown = Time.time + FL_Delay;
                StartCoroutine(MoveDash());
            }

            if (Vector3.Distance(Target.transform.position, transform.position) < 10.0f)
                RunFrom();
            else if (Vector3.Distance(Target.transform.position, transform.position) >= 10.0f)
                ChaseTo();
        }
	}

    void FixedUpdate()
    {
        RB_Enemy.MovePosition(transform.position + direction * Time.fixedDeltaTime);
    }

    void RandomDirection()
    {
        int rand = Random.Range(0, 1);

        if (rand == 0)
            moveInput = Vector3.left;
        else
            moveInput = Vector3.right;

        direction = moveInput.normalized * FL_moveSpeed;
    }

    IEnumerator MoveDash()
    {
        BL_Dash = true;
        RandomDirection();
        //Do the dash and decrease dash charges
        FL_moveSpeed = FL_defaultSpeed * 3;

        yield return new WaitForSeconds(0.2f);

        BL_Dash = false;
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
        NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetAreaFromName("Default"));
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
