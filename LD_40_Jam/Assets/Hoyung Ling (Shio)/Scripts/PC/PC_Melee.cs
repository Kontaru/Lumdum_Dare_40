using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Melee : MonoBehaviour {

    public int swings;
    public KeyCode KC_Attack;
    public int IN_Swings = 3;

    public bool BL_Staggered = false;

    public GameObject GO_MeleeCast;

	// Use this for initialization
	void Start () {
        IN_Swings = swings;
	}
	
	// Update is called once per frame
	void Update () {
        if (!BL_Staggered)
           if (Input.GetKeyDown(KC_Attack)) Attack();
	}

    void Attack()
    {
        IN_Swings--;
        Instantiate(GO_MeleeCast, transform.position + new Vector3(0, 1, 0), transform.rotation);
        if (IN_Swings == 0)
        {
            IN_Swings = swings;
            BL_Staggered = true;
        }
    }
}
