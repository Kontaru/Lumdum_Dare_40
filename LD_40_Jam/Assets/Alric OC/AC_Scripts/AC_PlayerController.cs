using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class AC_PlayerController : MonoBehaviour
{
    Vector3 velocity;
    Rigidbody RB_PC;

    // Use this for initialization
    void Start()
    {
        RB_PC = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 RB_velocity)
    {
        velocity = RB_velocity.normalized;
    }

    public void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }
    void FixedUpdate()
    {
        RB_PC.MovePosition(RB_PC.position + velocity * Time.deltaTime);
    }
}