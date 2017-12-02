using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {

	void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.SendMessage("TakeCoin", 1, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }
}
