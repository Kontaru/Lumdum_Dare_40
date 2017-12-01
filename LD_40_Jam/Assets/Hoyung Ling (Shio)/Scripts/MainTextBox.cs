using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTextBox : MonoBehaviour {

    [Header("Attached to the main text box of the scene")]
    public bool Main = false;

	void Awake () {
        if (Main == true)
            TextManager.instance.textBoxSetter(GetComponent<Text>());
	}
}
