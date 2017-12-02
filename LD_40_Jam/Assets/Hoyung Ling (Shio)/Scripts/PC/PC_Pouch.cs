using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PC_Pouch : MonoBehaviour {

    //Coin animation
    public Image coinGraphic;
    float timeOfTravel = 1; //time after object reach a target place 
    float currentTime = 0; // actual floting time 
    float timeinterval = 0;
    bool BL_Animate;
    bool BL_Increase;

    public Vector3 startPos;
    public Vector3 endPos;

    RectTransform myCoin = new RectTransform();
    public int coins;

	// Use this for initialization
	void Start () {
        myCoin = coinGraphic.GetComponent<RectTransform>();
        startPos = myCoin.anchoredPosition;
        endPos = myCoin.anchoredPosition + new Vector2(0, 100);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void TakeCoin(int value)
    {
        coins += value;
        BL_Animate = true;
        BL_Increase = true;
        StartCoroutine(CoinAnimation());
    }

    IEnumerator CoinAnimation()
    {
        while (BL_Animate == true)
        {
            if(BL_Increase)
                currentTime += Time.deltaTime;
            else
                currentTime -= Time.deltaTime;

            timeinterval = currentTime / timeOfTravel; // we normalize our time 

            if (timeinterval > 1)
                BL_Increase = !BL_Increase;

            if (timeinterval <= 0)
            {
                currentTime = 0;
                BL_Animate = false;
            }

            myCoin.anchoredPosition = Vector3.Lerp(startPos, endPos, timeinterval);
            yield return null;
        }
    }
}
