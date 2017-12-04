using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordLungeAnimation : MonoBehaviour {

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject != null && coll.gameObject.tag == "Player")
        {
            coll.gameObject.SendMessage("TakeDamage", 1, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        AudioManager.instance.Play("BossDamage");
    }

    void Update()
    {
        Destroy(gameObject, 0.2f);
    }
}
