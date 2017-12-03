using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class AC_Enemy : AC_Living
{
    public enum State { Idle, Chasing, Attacking, Roam, Search };
    State currentState;

    AC_FieldOfView fieldOfView;

    public GameObject deathEffect;
    public Transform[] points;
    private float mFL_moveSpeed;
    //public Vector3 desiredVelocity;
    //public float speed;

    UnityEngine.AI.NavMeshAgent pathfinder;
    Transform target;
    AC_Living targetEntity;

    AC_Player player;
    Material skinMaterial;

    Color originalColour;

    float attackDistanceThreshold = 1.5f;
    float timeBetweenAttacks = 1;
    int destPoint = 0;

    float nextAttackTime;
    float myCollisionRadius;
    float targetCollisionRadius;

    bool hasTarget;
    public bool heardTarget;

    //-----------------------------------------------------------
    public GameObject GO_Billboard;
    EnemyBillboard EB_Sprite;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        EB_Sprite = GO_Billboard.GetComponent<EnemyBillboard>();
        pathfinder = GetComponent<UnityEngine.AI.NavMeshAgent>();
        skinMaterial = GetComponent<Renderer>().material;
        originalColour = skinMaterial.color;
        currentState = State.Roam;
        fieldOfView = GetComponent<AC_FieldOfView>();
        mFL_moveSpeed = pathfinder.speed;
        player = GetComponent<AC_Player>();
        //desiredVelocity = pathfinder.speed * 5;

        pathfinder.autoBraking = false;

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            hasTarget = true;

            target = GameObject.FindGameObjectWithTag("Player").transform;
            targetEntity = target.GetComponent<AC_Living>();

            myCollisionRadius = GetComponent<CapsuleCollider>().radius;
            targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;

            InvokeRepeating("Chase", .1F, .1F);
        }
        //if (GameObject.FindGameObjectWithTag("Noise") != null)
        //{
        //    target = GameObject.FindGameObjectWithTag("Player").transform;
        //    targetEntity = target.GetComponent<AC_Living>();

        //    myCollisionRadius = GetComponent<CapsuleCollider>().radius;
        //    targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;

        //    InvokeRepeating("Search", .1F, .1F);
        //}
        GotoNextPoint();
    }


    // Update is called once per frame
    void Update()
    {
        if (fieldOfView.withinchasingrange == true)
        {
            if (Time.time > nextAttackTime)
            {
                float sqrDistanceTOTarget = (target.position - transform.position).sqrMagnitude;

                if (sqrDistanceTOTarget < Mathf.Pow(attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2))
                {
                    nextAttackTime = Time.time + timeBetweenAttacks;
                    StartCoroutine(Attack());
                }
            }
        }
        if (pathfinder.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
        Triggers();
    }

    void Triggers()
    {
        if (currentState == State.Roam)
        {
            if (fieldOfView.withinchasingrange == true)
            {
                EB_Sprite.BL_Spotted = true;
                currentState = State.Chasing;
            }
            else
            {
                EB_Sprite.BL_Spotted = false;
            }
        }
        else if (heardTarget == true)
        {
            currentState = State.Search;
        }
        /*if (player.currentState == AC_Player.State.Attack)
        {
            currentState = State.Idle;
        }*/
    }

    IEnumerator Attack()
    {
        currentState = State.Attacking;
        pathfinder.enabled = true;

        Vector3 originalPositon = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget;


        float attackSpeed = 3;
        float percent = 0;

        skinMaterial.color = Color.red;
        bool hasAppliedDamage = false;

        while (percent <= 1)
        {
            if (percent >= .5f && !hasAppliedDamage)
            {
                hasAppliedDamage = true;
            }
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPositon, attackPosition, interpolation);

            yield return null;
        }

        skinMaterial.color = originalColour;
        currentState = State.Chasing;
        pathfinder.enabled = true;
    }
    void Chase()
    {
        if (fieldOfView.withinchasingrange == true)
        {
            if (currentState == State.Chasing)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold / 2);

                if (!dead)
                {
                    pathfinder.speed = mFL_moveSpeed * 1.5f;
                    pathfinder.SetDestination(targetPosition);
                }
            }
        }
    }
    void GotoNextPoint()
    {
        pathfinder.enabled = true;

        if (currentState == State.Roam)
        {

            if (points.Length == 0)
            {
                return;
            }
            pathfinder.speed = mFL_moveSpeed;
            pathfinder.destination = points[destPoint].position;

            destPoint = (destPoint + 1) % points.Length;
        }
    }
    //void Search()
    //{
    //    if (heardTarget == true)
    //    {
    //        if (currentState == State.Search)
    //        {
    //            Vector3 dirToTarget = (target.position - transform.position).normalized;
    //            Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold / 2);

    //            if (!dead)
    //            {
    //                pathfinder.speed = mFL_moveSpeed * 0.8f;
    //                pathfinder.SetDestination(targetPosition);
    //            }
    //        }
    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        heardTarget = true;
        if (collision.gameObject.name == "Noise_Sphere")
        {
            if (currentState == State.Search)
            {
                {
                    Vector3 dirToTarget = (target.position - transform.position).normalized;
                    Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold / 2);

                    if (!dead)
                    {
                        pathfinder.speed = mFL_moveSpeed * 0.8f;
                        pathfinder.SetDestination(targetPosition);
                    }
                }
            }
        }
    }
}