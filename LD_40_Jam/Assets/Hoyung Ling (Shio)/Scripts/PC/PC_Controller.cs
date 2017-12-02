using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Controller : MonoBehaviour {

    public int health;
    int max_health;

    //Bool
    bool BL_Staggered = false;
    public float FL_StaggerTimer = 5.0f;

    //Controllers
    PC_Melee CC_Melee;
    PC_Move CC_Move;

	// Use this for initialization
	void Start () {
        CC_Melee = GetComponent<PC_Melee>();
        CC_Move = GetComponent<PC_Move>();
    }
	
	// Update is called once per frame
	void Update () {
        BL_Staggered = CC_Melee.BL_Staggered;
        CC_Move.BL_Staggered = BL_Staggered;

        if (BL_Staggered == true)
            StaggerTimer(FL_StaggerTimer);
	}

    IEnumerator StaggerTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        BL_Staggered = false;
        CC_Move.BL_Staggered = BL_Staggered;
        CC_Melee.BL_Staggered = BL_Staggered;
    }
}
