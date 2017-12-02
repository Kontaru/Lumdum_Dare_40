using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Melee : MonoBehaviour {

    public int swings;
    public int dashes;
    public KeyCode KC_Attack;
    public KeyCode KC_Dash;
    public int IN_Swings = 3;
    public int IN_Dashes = 2;
    public bool BL_Staggered = false;

	// Use this for initialization
	void Start () {
        IN_Swings = swings;
        IN_Dashes = dashes;
	}
	
	// Update is called once per frame
	void Update () {
        if (!BL_Staggered)
        {
            if (Input.GetKeyDown(KC_Attack)) Attack();
            if (Input.GetKeyDown(KC_Dash)) Dash();
        }

	}

    void Attack()
    {
        IN_Swings--;
        //Cast a melee swing
        if (IN_Swings == 0)
        {
            IN_Swings = swings;
            BL_Staggered = true;
        }
    }

    void Dash()
    {
        IN_Dashes--;
        //Move the player in a direction
        if (IN_Dashes == 0)
        {
            IN_Dashes = dashes;
            BL_Staggered = true;
        }
    }
}
