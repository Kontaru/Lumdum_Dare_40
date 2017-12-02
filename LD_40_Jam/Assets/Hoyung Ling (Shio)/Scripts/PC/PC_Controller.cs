using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Controller : MonoBehaviour {

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
		
	}
}
