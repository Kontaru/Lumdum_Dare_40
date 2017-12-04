using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class AC_Enemy : AC_Living
{
    public enum State { Idle, Searching, Chasing, Attacking, Roam };
    State currentState;

    AC_FieldOfView fieldOfView;
    AC_FieldOfNoise fieldOfNoise;

    public GameObject deathEffect;
    public Transform[] points;
    private float mFL_moveSpeed;
    //public Vector3 desiredVelocity;
    //public float speed;

    UnityEngine.AI.NavMeshAgent pathfinder;
    Transform target;
    //AC_Living targetEntity;


    AC_Player player;
    Material skinMaterial;

    Color originalColour;

    float attackDistanceThreshold = 1.5f;
    float timeBetweenAttacks = 1;
    float damage = 1;
    int destPoint = 0;

    float nextAttackTime;
    float myCollisionRadius;
    float targetCollisionRadius;

    bool hasTarget;
    bool BL_Moving;

    //HYL STUFF
    public GameObject PF_Slash;
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
        fieldOfNoise = GetComponent<AC_FieldOfNoise>();
        mFL_moveSpeed = pathfinder.speed;
        player = GetComponent<AC_Player>();
        fieldOfNoise.withinsearchrange = false;
        //desiredVelocity = pathfinder.speed * 5;

        pathfinder.autoBraking = false;

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            hasTarget = true;

            target = GameObject.FindGameObjectWithTag("Player").transform;
            //targetEntity = target.GetComponent<AC_Living>();
            //targetEntity.OnDeath += OnTargetDeath;

            myCollisionRadius = GetComponent<CapsuleCollider>().radius;
            targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;

            InvokeRepeating("Chase", .1F, .1F);
        }

        GotoNextPoint();
    }

    public override void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (damage >= health)
        {
            Destroy(Instantiate(deathEffect, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection)) as GameObject, 2);
        }
        base.TakeHit(damage, hitPoint, hitDirection);
    }

    void OnTargetDeath()
    {
        hasTarget = false;
        currentState = State.Idle;
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
        if (fieldOfNoise.withinsearchrange == true && BL_Moving == true)
        {
            Search();
        }
        if (pathfinder.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
        Triggers();

        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (moveInput != new Vector3(0, 0, 0))
        {
            BL_Moving = true;
        }
    }

    void Triggers()
    {
        if (currentState == State.Roam)
        {
            if (fieldOfView.withinchasingrange == true)
            {
                currentState = State.Chasing;
                EB_Sprite.BL_Spotted = true;
                fieldOfNoise.withinsearchrange = false;
            }
            if (fieldOfView.withinchasingrange == false)
            {
                if (fieldOfNoise.withinsearchrange == true && BL_Moving == true)
                {
                    EB_Sprite.BL_Spotted = true;
                    currentState = State.Searching;
                }
                else
                {
                    EB_Sprite.BL_Spotted = false;
                    currentState = State.Roam;
                }
            }
        }
        //if (player.currentState == AC_Player.State.Attack)
        //{
        //    currentState = State.Idle;
        //}
    }

    IEnumerator Attack()
    {
        currentState = State.Attacking;
        pathfinder.enabled = true;

        Vector3 originalPositon = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPosition = target.position - dirToTarget * (myCollisionRadius);


        float attackSpeed = 3;
        float percent = 0;

        skinMaterial.color = Color.red;
        bool hasAppliedDamage = false;

        while (percent <= 1)
        {
            if (percent >= .5f && !hasAppliedDamage)
            {
                hasAppliedDamage = true;
                //targetEntity.TakeDamage(damage);
                Instantiate(PF_Slash, transform.position + new Vector3(0, 1, 0), transform.rotation);
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
                    pathfinder.speed = mFL_moveSpeed * 2;
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
    void Search()
    {
        if (fieldOfNoise.withinsearchrange == true && BL_Moving == true)
        {
            if (currentState == State.Searching)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - dirToTarget;

                if (!dead)
                {
                    pathfinder.speed = mFL_moveSpeed * 0.8f;
                    pathfinder.SetDestination(targetPosition);
                }
            }
        }
    }
}