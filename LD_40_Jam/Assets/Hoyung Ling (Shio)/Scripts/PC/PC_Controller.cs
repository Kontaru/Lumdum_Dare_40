using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Controller : MonoBehaviour {

    //Bool
    public bool BL_Staggered = false;
    public bool BL_StaggerToggle = false;
    public float FL_StaggerTimer = 1.0f;
    public bool BL_IsMoving;

    //Controllers
    PC_Melee CC_Melee;
    PC_Move CC_Move;

	// Use this for initialization
	void Start () {
        AudioManager.instance.Stop("Theme");
        AudioManager.instance.Play("Dungeon Music");

        CC_Melee = GetComponent<PC_Melee>();
        CC_Move = GetComponent<PC_Move>();
    }
	
	// Update is called once per frame
	void Update () {

        BL_IsMoving = CC_Move.BL_Moving;
        if (CC_Melee.BL_Staggered == true)
        {
            BL_Staggered = CC_Melee.BL_Staggered;
            CC_Move.BL_Staggered = BL_Staggered;
        }

        if (CC_Move.BL_Staggered == true)
        {
            BL_Staggered = CC_Move.BL_Staggered;
            CC_Melee.BL_Staggered = BL_Staggered;
        }

        if (BL_Staggered)
        {
            if (BL_StaggerToggle == false)
            {
                StartCoroutine(StaggerTimer(FL_StaggerTimer));
                BL_StaggerToggle = true;
            }
        }
    }

    IEnumerator StaggerTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        BL_Staggered = false;
        CC_Move.BL_Staggered = BL_Staggered;
        CC_Move.ResetDash();
        CC_Melee.BL_Staggered = BL_Staggered;
        CC_Melee.ResetSwings();
        BL_StaggerToggle = false;
    }
}
