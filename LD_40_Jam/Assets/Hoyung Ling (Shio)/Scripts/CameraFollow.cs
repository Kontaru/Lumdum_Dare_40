using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    //Very simple code which makes an empty game object follow the player, 
    //To allow the main camera to tracking the player

    public bool BL_FollowPC;

    public GameObject GO_Body;          //Reference to player
    public GameObject lookAt;

    private float smoothSpeed = 1f;

    void Update () {
        PCFollowBody();
	}

    void PCFollowBody()
    {
        //Sets transform to the player
        if (BL_FollowPC)
        {
            if (Vector3.Distance(transform.position, GO_Body.transform.position) < 2.0f)
                transform.position = Vector3.Lerp(transform.position, GO_Body.transform.position, smoothSpeed * 5 * Time.deltaTime);
            else
                transform.position = Vector3.Lerp(transform.position, GO_Body.transform.position, smoothSpeed * Time.deltaTime);
        }
        //transform.position = Vector3.Lerp(transform.position, lookAt.transform.position, smoothSpeed * Time.deltaTime);
    }
}
