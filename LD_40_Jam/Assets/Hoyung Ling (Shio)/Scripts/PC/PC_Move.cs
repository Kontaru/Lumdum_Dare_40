using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Move : MonoBehaviour {

    public KeyCode KC_Forward;
    public KeyCode KC_Left;
    public KeyCode KC_Down;
    public KeyCode KC_Right;
    public bool BL_Staggered = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (BL_Staggered) return;

        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        /*Vector3 moveVelocity = moveInput.normalized * mFL_moveSpeed;
        controller.Move(moveVelocity);

        //Look Input
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane grounPlane = new Plane(Vector3.up, Vector3.up);
        float rayDistance;

        if (grounPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            //Debug.DrawLine(ray.origin,point, Color.red);
            controller.LookAt(point);
        }
        Attack();
        */
    }
}
