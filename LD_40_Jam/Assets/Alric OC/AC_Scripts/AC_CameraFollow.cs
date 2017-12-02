using UnityEngine;
using System.Collections;

public class AC_CameraFollow : MonoBehaviour 
{
    public Vector3 offset = new Vector3(0, 10, -5);
    public Transform lookAt;
    public Transform target;
    public Transform cameraTransform;

    private bool smooth = true;
    private float smoothSpeed = 5f;
    private bool PC_target;

	// Use this for initialization
	private void Start () 
    {
        PC_target = true;
        gameObject.transform.position = cameraTransform.position;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.B))
        {
            PC_target = false;
        }
        if (Input.GetKey(KeyCode.N))
        {
            PC_target = true;
        }
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (PC_target == true)
        {
            Vector3 desiredPosition = Vector3.Lerp(cameraTransform.position, lookAt.transform.position + offset, smoothSpeed * Time.deltaTime);

            transform.position = desiredPosition;
        }
        else
        {
            Vector3 desiredPosition = Vector3.Lerp(cameraTransform.position, target.transform.position + offset, smoothSpeed * Time.deltaTime);

            transform.position = desiredPosition;
        }
    }
}
