using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    Rigidbody RB_Bullet;
    public float FL_Speed;
    public int IN_Damage;

    // Use this for initialization
    void Start () {

        RB_Bullet = GetComponent<Rigidbody>();
        RB_Bullet.velocity = transform.TransformDirection(Vector3.forward) * FL_Speed;

        Destroy(gameObject, 6.0f);
	}

    void OnTriggerEnter(Collider coll)
    {
        if (coll != null)
        {
            if (coll.gameObject.tag == "Player")
            {
                coll.gameObject.SendMessage("TakeDamage", IN_Damage, SendMessageOptions.DontRequireReceiver);
                Destroy(gameObject);
            }
            //If any other entity is hit by the bullet, the bullet will not be destroyed!
        }
        else
        {
            Debug.Log("I hit something");
            Destroy(gameObject);
        }
    }
}
