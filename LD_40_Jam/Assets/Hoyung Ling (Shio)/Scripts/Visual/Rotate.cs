﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float degree;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, degree, 0));
	}
}
