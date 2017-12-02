using UnityEngine;
using System.Collections;

public class AC_CameraFollow : MonoBehaviour 
{
    public Transform lookAt;

    private bool smooth = true;
    private float smoothSpeed = 0.125f;
    private Vector3 offset = new Vector3(0,10,-5);

	// Use this for initialization
	private void Start () 
    {

	}

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 desiredPosition = lookAt.transform.position + offset;

        transform.position = desiredPosition;
    }
}
