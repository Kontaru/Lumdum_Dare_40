using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCount : MonoBehaviour {

    Text coinCount;

	// Use this for initialization
	void Start () {
        coinCount = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        coinCount.text = string.Format(PC_Pouch.coins.ToString());
	}
}
