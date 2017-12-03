using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack : MonoBehaviour {

    public GameObject PF_Bullet;
    public GameObject GO_EjectionPoint;

    public bool BL_Target = false;                  //Do we target?
    private float FL_Shoot = 0;             //The value which Time.time compares itself to to know when to shoot
    protected float FL_CoolDown = 3f;       //Value added to Time.time which creates FL_Shoot, to determine the space between shoots

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        FireAtPC();
	}

    #region --- Spawn Bullet ---

    //------------------------------------------------------------
    //Instantiates a bullet at the tip of the gun (just the tip)
    //------------------------------------------------------------
    protected void SpawnBullet()
    {
        //Generate a bullet at the ejection point and play some audio
        Instantiate(PF_Bullet, GO_EjectionPoint.transform.position, GO_EjectionPoint.transform.rotation);
    }

    #endregion

    #region --- Begin Shooting ---

    void FireAtPC()
    {
        if (BL_Target)                                                                                              //If we're allowed to target
        {
            if (Time.time > FL_Shoot)                                                                               //If the time is right...
            {
                SpawnBullet();                                                                                      //Spawn a bullet
                FL_Shoot = Time.time + FL_CoolDown;                                                                 //Add a cooldown
            }
        }
    }
    #endregion
}
