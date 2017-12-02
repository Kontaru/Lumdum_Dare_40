using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : MonoBehaviour {
	
	void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject != null)
        {
            coll.gameObject.SendMessage("TakeDamage", 1, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        Destroy(gameObject, 0.2f);
    }
}
