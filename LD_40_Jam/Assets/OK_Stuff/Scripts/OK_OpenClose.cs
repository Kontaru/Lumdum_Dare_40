using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OK_OpenClose : MonoBehaviour {

    // Setting Gameobject variables to be used.
    public GameObject mGO_Player;
    public GameObject mGO_Target;

    public bool bl_Enable;
    
    // Use this for initialization
	void Start () {

        mGO_Player = GameObject.FindGameObjectWithTag("Player");

        if (bl_Enable == true)
            mGO_Target.SetActive(false);


	}

    private void OnTriggerEnter(Collider cl_trigger)
    {
        if (cl_trigger.gameObject == mGO_Player)
        {
            Destroy(gameObject);
            if (bl_Enable == true)
            {
                mGO_Target.SetActive(true);
            }
            else if (bl_Enable == false)
            {
                mGO_Target.SetActive(false);
            }

        }
    }

}
