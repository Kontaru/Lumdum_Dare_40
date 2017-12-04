using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (coll.gameObject.GetComponent<PC_Health>().health < PC_Health.max_health)
            {
                coll.gameObject.SendMessage("TakeHealth", 1, SendMessageOptions.DontRequireReceiver);
                Destroy(gameObject);
            }
        }
    }
}
