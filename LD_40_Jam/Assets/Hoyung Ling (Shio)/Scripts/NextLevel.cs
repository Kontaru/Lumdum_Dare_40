using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour {

	void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            GameManager.instance.NextScene();
        }
    }
}
