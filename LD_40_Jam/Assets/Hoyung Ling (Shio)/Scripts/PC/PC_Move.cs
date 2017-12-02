using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Move : MonoBehaviour {

    [Header("Dashes")]
    public int dashes;
    public KeyCode KC_Dash;
    public int IN_Dashes = 2;
    bool BL_Dash = false;
    bool BL_HasDashed = false;

    Rigidbody RB_PC;
    Vector3 direction;

    [Header("Movement")]
    public bool BL_Staggered = false;
    public float FL_moveSpeed;
    float FL_defaultSpeed;

    // Use this for initialization
    void Start () {
        RB_PC = GetComponent<Rigidbody>();
        IN_Dashes = dashes;
        FL_defaultSpeed = FL_moveSpeed;
    }
	
	// Update is called once per frame
	void Update () {

        StaggerCheck();

        PlayerMove();
        LookInput();

    }

    void FixedUpdate()
    {
        RB_PC.MovePosition(transform.position + direction * Time.fixedDeltaTime);
    }

    void StaggerCheck()
    {
        if (BL_Staggered)
        {
            FL_moveSpeed = FL_defaultSpeed / 5;
        }
        else if (Input.GetKeyDown(KC_Dash) && BL_Dash == false)
            StartCoroutine(MoveDash());
        else if (BL_Dash == false)
        {
            FL_moveSpeed = FL_defaultSpeed;
        }
    }

    void PlayerMove()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        direction = moveInput.normalized * FL_moveSpeed;
    }

    void LookInput()
    {
        //Look Input
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane grounPlane = new Plane(Vector3.up, Vector3.up);
        float rayDistance;

        if (grounPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Vector3 heightCorrectedPoint = new Vector3(point.x, transform.position.y, point.z);
            transform.LookAt(heightCorrectedPoint);
        }
    }

    IEnumerator MoveDash()
    {
        BL_Dash = true;

        //Do the dash and decrease dash charges
        IN_Dashes--;
        FL_moveSpeed = FL_defaultSpeed * 3;

        yield return new WaitForSeconds(0.2f);

        //If dash charges is 0, stun the player
        if (IN_Dashes == 0)
        {
            IN_Dashes = dashes;
            BL_Staggered = true;
        }

        BL_HasDashed = false;
        BL_Dash = false;
    }
}
